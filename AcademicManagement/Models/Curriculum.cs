using System.Collections.Generic;
using System.Linq;

namespace AcademicManagement.Models
{
    // Corresponds to a record in the "curriculum" node:
    // { "CurriculumID": "...", "ProgramId": "...", "SubjectCode": "...", "YearLevel": 1, "Semester": "1st" }
    public class Curriculum : AcademicRecord
    {
        public string CurriculumID { get; set; }
        public string ProgramId { get; set; }
        public string SubjectCode { get; set; }
        public int YearLevel { get; set; }
        public string Semester { get; set; }

        public override string GetId() => CurriculumID;

        // A Curriculum entry must reference an existing ProgramId and SubjectCode
        // (Day 1 rule: "this is where everything gets tied together").
        public bool Validate(out string errorMessage, IEnumerable<string> existingProgramIds, IEnumerable<string> existingSubjectCodes)
        {
            if (string.IsNullOrWhiteSpace(CurriculumID))
            {
                errorMessage = "CurriculumID is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(ProgramId))
            {
                errorMessage = "ProgramId is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(SubjectCode))
            {
                errorMessage = "SubjectCode is required.";
                return false;
            }
            if (YearLevel <= 0)
            {
                errorMessage = "YearLevel must be a positive number.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Semester))
            {
                errorMessage = "Semester is required.";
                return false;
            }
            if (existingProgramIds != null && !existingProgramIds.Contains(ProgramId))
            {
                errorMessage = $"ProgramId '{ProgramId}' does not exist. Add the Program first.";
                return false;
            }
            if (existingSubjectCodes != null && !existingSubjectCodes.Contains(SubjectCode))
            {
                errorMessage = $"SubjectCode '{SubjectCode}' does not exist. Add the Subject first.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        public override bool Validate(out string errorMessage)
        {
            return Validate(out errorMessage, null, null);
        }

        public override Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "CurriculumID", CurriculumID },
                { "ProgramId", ProgramId },
                { "SubjectCode", SubjectCode },
                { "YearLevel", YearLevel },
                { "Semester", Semester }
            };
        }
    }
}
