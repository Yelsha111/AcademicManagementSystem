using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AcademicManagement.Models;
using AcademicManagement.Services;

namespace AcademicManagement.Managers
{
    // Controller class from the Day 2 class diagram.
    // Hides how records are searched, validated, ID-generated, and written to
    // Firebase (abstraction) — the forms only ever call these methods, never
    // FirebaseService directly.
    //
    // All primary keys (CollegeId, ProgramId, SubjectCode, CurriculumID,
    // OfferingID) are AUTO-GENERATED here, per the JSON Structure doc.
    // The user never types an ID by hand - AddXAsync() builds it automatically
    // as prefix + next available number (e.g. CLG001, CLG002, ...).
    public class AcademicManager
    {
        private readonly FirebaseService _firebase;

        public AcademicManager(FirebaseService firebase)
        {
            _firebase = firebase;
        }

        // Finds the next available ID for a given prefix, e.g.
        // GenerateNextId(["CLG001","CLG002"], "CLG") -> "CLG003"
        // GenerateNextId([], "CLG") -> "CLG001"
        private static string GenerateNextId(IEnumerable<string> existingIds, string prefix, int digits = 3)
        {
            var pattern = new Regex("^" + Regex.Escape(prefix) + @"(\d+)$");
            int max = 0;

            foreach (var id in existingIds ?? Enumerable.Empty<string>())
            {
                var match = pattern.Match(id ?? "");
                if (match.Success && int.TryParse(match.Groups[1].Value, out var num))
                    max = Math.Max(max, num);
            }

            return prefix + (max + 1).ToString(new string('0', digits));
        }

        // ---------- Colleges ----------

        public async Task<List<College>> GetCollegesAsync()
        {
            var dict = await _firebase.GetAllAsync<College>("colleges");
            return dict.Values.ToList();
        }

        // Adds a new College. CollegeId is generated automatically (CLG001, CLG002, ...).
        public async Task<(bool success, string error, string newId)> AddCollegeAsync(string collegeName)
        {
            var existing = await GetCollegesAsync();
            var newId = GenerateNextId(existing.Select(c => c.CollegeId), "CLG");

            var college = new College { CollegeId = newId, CollegeName = collegeName };
            if (!college.Validate(out var error, existing))
                return (false, error, null);

            var ok = await _firebase.SaveAsync("colleges", newId, college.ToDictionary());
            return (ok, ok ? null : "Failed to save College to Firebase.", ok ? newId : null);
        }

        // Updates an existing College (CollegeId is fixed, cannot be changed here).
        public async Task<(bool success, string error)> UpdateCollegeAsync(College college)
        {
            var existing = await GetCollegesAsync();
            if (!college.Validate(out var error, existing, excludeId: college.CollegeId))
                return (false, error);

            var ok = await _firebase.SaveAsync("colleges", college.CollegeId, college.ToDictionary());
            return (ok, ok ? null : "Failed to update College in Firebase.");
        }

        public Task<bool> DeleteCollegeAsync(string collegeId) =>
            _firebase.DeleteAsync("colleges", collegeId);

        // ---------- Programs ----------

        public async Task<List<Models.Program>> GetProgramsAsync()
        {
            var dict = await _firebase.GetAllAsync<Models.Program>("programs");
            return dict.Values.ToList();
        }

        // Adds a new Program. ProgramId is generated automatically (PRG001, PRG002, ...).
        public async Task<(bool success, string error, string newId)> AddProgramAsync(string programName, string collegeId)
        {
            var colleges = await GetCollegesAsync();
            var existingPrograms = await GetProgramsAsync();
            var newId = GenerateNextId(existingPrograms.Select(p => p.ProgramId), "PRG");

            var program = new Models.Program { ProgramId = newId, ProgramName = programName, CollegeId = collegeId };
            if (!program.Validate(out var error, colleges.Select(c => c.CollegeId)))
                return (false, error, null);

            var ok = await _firebase.SaveAsync("programs", newId, program.ToDictionary());
            return (ok, ok ? null : "Failed to save Program to Firebase.", ok ? newId : null);
        }

        public async Task<(bool success, string error)> UpdateProgramAsync(Models.Program program)
        {
            var colleges = await GetCollegesAsync();
            if (!program.Validate(out var error, colleges.Select(c => c.CollegeId)))
                return (false, error);

            var ok = await _firebase.SaveAsync("programs", program.ProgramId, program.ToDictionary());
            return (ok, ok ? null : "Failed to update Program in Firebase.");
        }

        public Task<bool> DeleteProgramAsync(string programId) =>
            _firebase.DeleteAsync("programs", programId);

        // ---------- Subjects (Courses) ----------

        // Adds a new Subject. SubjectCode is generated automatically as
        // prefix + next available number for that prefix (e.g. IT101, IT102,
        // CS101, GE101, ...). "prefix" is chosen by the user in the form
        // (e.g. IT, CS, GE) since it reflects the subject's department/category.
        public async Task<(bool success, string error, string newId)> AddSubjectAsync(string prefix, string description, int units, List<string> prerequisites)
        {
            var existing = await GetSubjectsAsync();
            var sameCategory = existing.Where(s => s.SubjectCode.StartsWith(prefix)).Select(s => s.SubjectCode);
            var newId = GenerateNextId(sameCategory, prefix);

            var subject = new Subject { SubjectCode = newId, SubjectDescription = description, Units = units, Prerequisites = prerequisites };
            if (!subject.Validate(out var error, existing.Select(s => s.SubjectCode)))
                return (false, error, null);

            var ok = await _firebase.SaveAsync("courses", newId, subject.ToDictionary());
            return (ok, ok ? null : "Failed to save Subject to Firebase.", ok ? newId : null);
        }

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            var dict = await _firebase.GetAllAsync<Subject>("courses");
            return dict.Values.ToList();
        }

        public async Task<(bool success, string error)> UpdateSubjectAsync(Subject subject)
        {
            var existing = await GetSubjectsAsync();
            if (!subject.Validate(out var error, existing.Select(s => s.SubjectCode)))
                return (false, error);

            var ok = await _firebase.SaveAsync("courses", subject.SubjectCode, subject.ToDictionary());
            return (ok, ok ? null : "Failed to update Subject in Firebase.");
        }

        public Task<bool> DeleteSubjectAsync(string subjectCode) =>
            _firebase.DeleteAsync("courses", subjectCode);

        // ---------- Curriculum ----------

        public async Task<List<Curriculum>> GetCurriculumAsync()
        {
            var dict = await _firebase.GetAllAsync<Curriculum>("curriculum");
            return dict.Values.ToList();
        }

        // Adds a new Curriculum entry. CurriculumID is generated automatically (CUR001, CUR002, ...).
        public async Task<(bool success, string error, string newId)> AddCurriculumAsync(string programId, string subjectCode, int yearLevel, string semester)
        {
            var programs = await GetProgramsAsync();
            var subjects = await GetSubjectsAsync();
            var existing = await GetCurriculumAsync();
            var newId = GenerateNextId(existing.Select(c => c.CurriculumID), "CUR");

            var curriculum = new Curriculum { CurriculumID = newId, ProgramId = programId, SubjectCode = subjectCode, YearLevel = yearLevel, Semester = semester };
            if (!curriculum.Validate(out var error, programs.Select(p => p.ProgramId), subjects.Select(s => s.SubjectCode)))
                return (false, error, null);

            var ok = await _firebase.SaveAsync("curriculum", newId, curriculum.ToDictionary());
            return (ok, ok ? null : "Failed to save Curriculum entry to Firebase.", ok ? newId : null);
        }

        public async Task<(bool success, string error)> UpdateCurriculumAsync(Curriculum curriculum)
        {
            var programs = await GetProgramsAsync();
            var subjects = await GetSubjectsAsync();
            if (!curriculum.Validate(out var error, programs.Select(p => p.ProgramId), subjects.Select(s => s.SubjectCode)))
                return (false, error);

            var ok = await _firebase.SaveAsync("curriculum", curriculum.CurriculumID, curriculum.ToDictionary());
            return (ok, ok ? null : "Failed to update Curriculum entry in Firebase.");
        }

        public Task<bool> DeleteCurriculumAsync(string curriculumId) =>
            _firebase.DeleteAsync("curriculum", curriculumId);

        // ---------- Academic Offerings ----------

        public async Task<List<AcademicOffering>> GetOfferingsAsync()
        {
            var dict = await _firebase.GetAllAsync<AcademicOffering>("academicofferings");
            return dict.Values.ToList();
        }

        // Adds a new Academic Offering. OfferingID is generated automatically (OFR001, OFR002, ...).
        public async Task<(bool success, string error, string newId)> AddOfferingAsync(string subjectCode, string schoolYear, string semester, string status)
        {
            var curriculum = await GetCurriculumAsync();
            var existing = await GetOfferingsAsync();
            var newId = GenerateNextId(existing.Select(o => o.OfferingID), "OFR");

            var offering = new AcademicOffering { OfferingID = newId, SubjectCode = subjectCode, SchoolYear = schoolYear, Semester = semester, Status = status };
            if (!offering.Validate(out var error, curriculum))
                return (false, error, null);

            var ok = await _firebase.SaveAsync("academicofferings", newId, offering.ToDictionary());
            return (ok, ok ? null : "Failed to save Academic Offering to Firebase.", ok ? newId : null);
        }

        public async Task<(bool success, string error)> UpdateOfferingAsync(AcademicOffering offering)
        {
            var curriculum = await GetCurriculumAsync();
            if (!offering.Validate(out var error, curriculum))
                return (false, error);

            var ok = await _firebase.SaveAsync("academicofferings", offering.OfferingID, offering.ToDictionary());
            return (ok, ok ? null : "Failed to update Academic Offering in Firebase.");
        }

        public Task<bool> DeleteOfferingAsync(string offeringId) =>
            _firebase.DeleteAsync("academicofferings", offeringId);

        // ---------- Search (simple case-insensitive filter, used by all 5 screens) ----------

        public List<College> SearchColleges(List<College> source, string keyword) =>
            string.IsNullOrWhiteSpace(keyword)
                ? source
                : source.Where(c => c.CollegeId.ToLower().Contains(keyword.ToLower())
                                  || c.CollegeName.ToLower().Contains(keyword.ToLower())).ToList();

        public List<Models.Program> SearchPrograms(List<Models.Program> source, string keyword) =>
            string.IsNullOrWhiteSpace(keyword)
                ? source
                : source.Where(p => p.ProgramId.ToLower().Contains(keyword.ToLower())
                                  || p.ProgramName.ToLower().Contains(keyword.ToLower())).ToList();

        public List<Subject> SearchSubjects(List<Subject> source, string keyword) =>
            string.IsNullOrWhiteSpace(keyword)
                ? source
                : source.Where(s => s.SubjectCode.ToLower().Contains(keyword.ToLower())
                                  || s.SubjectDescription.ToLower().Contains(keyword.ToLower())).ToList();
    }
}