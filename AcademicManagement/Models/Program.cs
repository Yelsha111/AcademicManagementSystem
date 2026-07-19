using System.Collections.Generic;
using System.Linq;

namespace AcademicManagement.Models
{
    // Corresponds to a record in the "programs" node:
    // { "ProgramId": "...", "ProgramName": "...", "CollegeId": "...", "Abbreviation": "..." }
    public class Program : AcademicRecord
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string CollegeId { get; set; }

        // Short code used as a Subject-code prefix (e.g. "BSIT", "BSCS", "BSCE").
        // Set once when the Program is created; used later by SubjectForm to build
        // its Category dropdown dynamically (GE + every Program's Abbreviation).
        public string Abbreviation { get; set; }

        public override string GetId() => ProgramId;

        // A Program must always reference an existing CollegeId (Day 1 rule:
        // "a program can never exist floating without a parent college").
        // Pass in the list of existing CollegeIds so this can be checked here.
        public bool Validate(out string errorMessage, IEnumerable<string> existingCollegeIds, IEnumerable<Program> existingPrograms = null, string excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(ProgramId))
            {
                errorMessage = "ProgramId is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(ProgramName))
            {
                errorMessage = "ProgramName is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(CollegeId))
            {
                errorMessage = "CollegeId is required.";
                return false;
            }
            if (existingCollegeIds != null && !existingCollegeIds.Contains(CollegeId))
            {
                errorMessage = $"CollegeId '{CollegeId}' does not exist. Add the College first.";
                return false;
            }

            if (existingPrograms != null)
            {
                var duplicate = existingPrograms.FirstOrDefault(p =>
                    p.ProgramId != excludeId &&
                    string.Equals(p.ProgramName.Trim(), ProgramName.Trim(), System.StringComparison.OrdinalIgnoreCase));

                if (duplicate != null)
                {
                    errorMessage = $"A program named \"{ProgramName.Trim()}\" already exists ({duplicate.ProgramId}).";
                    return false;
                }
            }

            errorMessage = null;
            return true;
        }

        public override bool Validate(out string errorMessage)
        {
            return Validate(out errorMessage, null);
        }

        public override Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "ProgramId", ProgramId },
                { "ProgramName", ProgramName },
                { "CollegeId", CollegeId },
                { "Abbreviation", Abbreviation }
            };
        }
    }
}

