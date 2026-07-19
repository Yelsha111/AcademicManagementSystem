using System.Collections.Generic;
using System.Linq;

namespace AcademicManagement.Models
{
    // Corresponds to a record in the "colleges" node:
    // { "CollegeId": "...", "CollegeName": "..." }
    public class College : AcademicRecord
    {
        public string CollegeId { get; set; }
        public string CollegeName { get; set; }

        public override string GetId() => CollegeId;

        // Checks required fields AND duplicate CollegeName (case-insensitive,
        // ignoring extra spaces) against every other existing college.
        // excludeId lets Update skip comparing a record against itself.
        public bool Validate(out string errorMessage, IEnumerable<College> existingColleges, string excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(CollegeId))
            {
                errorMessage = "CollegeId is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(CollegeName))
            {
                errorMessage = "CollegeName is required.";
                return false;
            }

            if (existingColleges != null)
            {
                bool isDuplicate = existingColleges.Any(c =>
                    c.CollegeId != excludeId &&
                    string.Equals(c.CollegeName.Trim(), CollegeName.Trim(), System.StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    errorMessage = $"A college named \"{CollegeName.Trim()}\" already exists ({existingColleges.First(c => string.Equals(c.CollegeName.Trim(), CollegeName.Trim(), System.StringComparison.OrdinalIgnoreCase)).CollegeId}).";
                    return false;
                }
            }

            errorMessage = null;
            return true;
        }

        // Base-class override (no duplicate check available without the existing list).
        public override bool Validate(out string errorMessage)
        {
            return Validate(out errorMessage, null);
        }

        public override Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "CollegeId", CollegeId },
                { "CollegeName", CollegeName }
            };
        }
    }
}

