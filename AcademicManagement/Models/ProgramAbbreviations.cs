using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademicManagement.Models
{
    // Maps full Program names to short prefixes (2-5 letters, no "BS"/"Bachelor of
    // Science in" prefix included) - same style as the existing Subject category
    // prefixes ("IT", "CS", "GE"). Used by AcademicManager to auto-fill
    // Program.Abbreviation when a Program is added, based on the two program lists
    //  provided (College of Engineering/CICS-adjacent list, and the second
    // College's list).
    //
    // If a ProgramName isn't in this table (e.g. a brand new program your team
    // didn't list yet), GetAbbreviation() falls back to auto-building a short code
    // from the program's initials, so AddProgramAsync never breaks.
    public static class ProgramAbbreviations
    {
        private static readonly Dictionary<string, string> KnownAbbreviations =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "BS Information Technology", "IT" },
            { "BS Computer Science", "CS" },
            { "BS Civil Engineering", "CE" },
            { "BS Business Administration - Financial Management", "BAFM" },
            { "Bachelor of Elementary Education", "EED" },
            { "Bachelor of Secondary Education", "SED" },
            { "BS Information Systems", "IS" },
            { "BS Mechanical Engineering", "ME" },
            { "BS Computer Engineering", "CPE" },
            { "BS Electrical Engineering", "EE" },
            { "BS Electronics Engineering", "ECE" },
            { "BS Industrial Engineering", "IE" },
            { "BS Business Administration - Marketing Management", "BAMM" },
            { "BS Accountancy", "ACC" },
            { "BS Tourism Management", "TM" },
            { "BS Hospitality Management", "HM" },
            { "BS Psychology", "PSY" },
            { "BS Criminology", "CRIM" },
            { "BS Biology", "BIO" },
            { "BS Computer Engineering Technology", "CET" },
            { "BS Nursing", "NUR" },
            { "BS Agriculture", "AGR" },
        };

        // Words skipped when building a fallback abbreviation from initials, so
        // "BS Business Administration - Marketing Management" doesn't turn into
        // something like "BSBAMM" (the leading "BS" isn't wanted per your request).
        private static readonly HashSet<string> SkipWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "BS", "Bachelor", "of", "in", "and", "-", "the"
        };

        public static string GetAbbreviation(string programName)
        {
            if (string.IsNullOrWhiteSpace(programName))
                return "GEN";

            if (KnownAbbreviations.TryGetValue(programName.Trim(), out var known))
                return known;

            // Fallback: build initials from the significant words only, e.g.
            // "BS Marine Biology" -> "MB" (skips "BS").
            var initials = programName
                .Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => !SkipWords.Contains(word))
                .Select(word => char.ToUpperInvariant(word[0]))
                .ToArray();

            return initials.Length > 0 ? new string(initials) : "GEN";
        }

        // Exposed so SubjectForm can eventually build its Category dropdown as
        // "GE" + every distinct Program abbreviation, instead of the hardcoded
        // IT/CS/GE list it uses today.
        public static IReadOnlyDictionary<string, string> All => KnownAbbreviations;
    }
}
