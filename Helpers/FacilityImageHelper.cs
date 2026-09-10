using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunitySportSystem.Helpers
{
    public static class FacilityImageHelper
    {
        private static readonly List<(string Keyword, string ImagePath)> KeywordImages =
            new List<(string, string)>
        {
            ("tennis",     "~/Content/Images/facilities/tennis.jfif"),
            ("football",   "~/Content/Images/facilities/football.jfif"),
            ("soccer",     "~/Content/Images/facilities/football.jfif"),
            ("basketball", "~/Content/Images/facilities/basketball.jfif"),
            ("swimming",   "~/Content/Images/facilities/swimming.jfif"),
            ("badminton",  "~/Content/Images/facilities/badminton.jfif"),
            ("volleyball", "~/Content/Images/facilities/volleyball.jfif"),
            ("cricket",    "~/Content/Images/facilities/volleyball.jfif"),
            ("gym",        "~/Content/Images/facilities/volleyball.jfif"),
            ("athletics",  "~/Content/Images/facilities/volleyball.jfif"),
        };

        private const string Fallback = "~/Content/Images/facilities/volleyball.jfif";
        public static string GetImageUrl(string facilityName, string typeName = null)
        {
            var haystack = ((facilityName ?? "") + " " + (typeName ?? "")).ToLowerInvariant();

            foreach (var entry in KeywordImages)
            {
                if (haystack.Contains(entry.Keyword))
                {
                    return entry.ImagePath;
                }
            }

            return Fallback;
        }
    }
}
