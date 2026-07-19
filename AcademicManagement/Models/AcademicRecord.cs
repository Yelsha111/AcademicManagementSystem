using System.Collections.Generic;

namespace AcademicManagement.Models
{
    // Shared base class for the five entities (Day 2 Class Diagram: <abstract> AcademicRecord).
    // Every entity must be able to identify itself (GetId), validate itself before
    // saving (Validate), and turn itself into a plain dictionary for Firebase (ToDictionary).
    public abstract class AcademicRecord
    {
        public abstract string GetId();
        public abstract bool Validate(out string errorMessage);
        public abstract Dictionary<string, object> ToDictionary();
    }
}
