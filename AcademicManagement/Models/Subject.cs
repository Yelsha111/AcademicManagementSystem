using System.Collections.Generic;
using System.Linq;

namespace AcademicManagement.Models
{
    // Corresponds to a record in the "courses" node:
    // { "SubjectCode": "...", "SubjectDescription": "...", "Units": 3, "Prerequisites": [...] }
    public class Subject : AcademicRecord
    {
        public string SubjectCode { get; set; }
        public string SubjectDescription { get; set; }
        public int Units { get; set; }
        public List<string> Prerequisites { get; set; } = new List<string>();

        // Read-only, comma-joined version of Prerequisites (e.g. "IT101, IT201").
        // Exists ONLY so the DataGridView's AutoGenerateColumns has something it
        // can actually display - WinForms silently skips List<T> properties like
        // Prerequisites itself when auto-generating columns, since it can't
        // flatten a list into a single cell. This plain string can.
        public string PrerequisitesDisplay =>
            (Prerequisites != null && Prerequisites.Count > 0)
                ? string.Join(", ", Prerequisites)
                : "None";

        public override string GetId() => SubjectCode;

        // Prerequisites (if any) must reference existing SubjectCodes.
        public bool Validate(out string errorMessage, IEnumerable<string> existingSubjectCodes)
        {
            if (string.IsNullOrWhiteSpace(SubjectCode))
            {
                errorMessage = "SubjectCode is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(SubjectDescription))
            {
                errorMessage = "SubjectDescription is required.";
                return false;
            }
            if (Units <= 0)
            {
                errorMessage = "Units must be a positive number.";
                return false;
            }
            if (existingSubjectCodes != null && Prerequisites != null)
            {
                var missing = Prerequisites.Where(p => !existingSubjectCodes.Contains(p)).ToList();
                if (missing.Any())
                {
                    errorMessage = $"Prerequisite(s) not found: {string.Join(", ", missing)}";
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
                { "SubjectCode", SubjectCode },
                { "SubjectDescription", SubjectDescription },
                { "Units", Units },
                { "Prerequisites", Prerequisites ?? new List<string>() }
            };
        }
    }
}
