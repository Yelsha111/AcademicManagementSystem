using System.Collections.Generic;
using System.Linq;

namespace AcademicManagement.Models
{
    // Corresponds to a record in the "academicofferings" node:
    // { "OfferingID": "...", "SubjectCode": "...", "SchoolYear": "...", "Semester": "...", "Status": "..." }
    public class AcademicOffering : AcademicRecord
    {
        public string OfferingID { get; set; }
        public string SubjectCode { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Status { get; set; }

        public override string GetId() => OfferingID;

        // Day 1 rule: "check if a subject is part of the curriculum before opening it
        // as an offering" - extended so the Offering's Semester must also match a
        // Semester the subject actually has in Curriculum (a subject can appear in
        // Curriculum more than once, under different Programs/Semesters, so ANY
        // matching Semester is accepted).
        public bool Validate(out string errorMessage, IEnumerable<Curriculum> curriculum)
        {
            if (string.IsNullOrWhiteSpace(OfferingID))
            {
                errorMessage = "OfferingID is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(SubjectCode))
            {
                errorMessage = "SubjectCode is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(SchoolYear))
            {
                errorMessage = "SchoolYear is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Semester))
            {
                errorMessage = "Semester is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Status))
            {
                errorMessage = "Status is required.";
                return false;
            }

            if (curriculum != null)
            {
                var matchesSubject = curriculum.Where(c => c.SubjectCode == SubjectCode).ToList();

                if (!matchesSubject.Any())
                {
                    errorMessage = $"SubjectCode '{SubjectCode}' is not part of the Curriculum yet. Add it to Curriculum first.";
                    return false;
                }

                if (!matchesSubject.Any(c => c.Semester == Semester))
                {
                    var validSemesters = string.Join(", ", matchesSubject.Select(c => c.Semester).Distinct());
                    errorMessage = $"SubjectCode '{SubjectCode}' is only in the Curriculum for: {validSemesters}. It cannot be offered in '{Semester}'.";
                    return false;
                }
            }

            errorMessage = null;
            return true;
        }

        // Base-class override (no reference list available) - basic field checks only.
        public override bool Validate(out string errorMessage)
        {
            return Validate(out errorMessage, null);
        }

        public override Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "OfferingID", OfferingID },
                { "SubjectCode", SubjectCode },
                { "SchoolYear", SchoolYear },
                { "Semester", Semester },
                { "Status", Status }
            };
        }
    }
}