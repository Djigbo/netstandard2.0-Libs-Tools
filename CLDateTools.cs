using System;
using System.Globalization;

namespace Tools
{
    public static class CLDateTools
    {
        /// <summary>
        /// This string array holds the date formats that are valid
        /// </summary>
        /// <remarks>These formats are used by <see cref="CLDateTools.ValidateDateString(string)"/> </remarks>
        private static readonly string[] Formats =
        {
            "M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
            "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
            "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
            "M/d/yyyy h:mm", "M/d/yyyy h:mm",
            "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
            "dd/MM/yyyy", "d/M/yyyy"
        };

        public static TimeSpan? GetDiffStringDate(string fromdate)
        {
            var dtFromDate = ValidateDateString(fromdate);
            var dtToDate = DateTime.Now;
            if (dtFromDate.HasValue)
            {
                TimeSpan timeSpan = dtToDate - dtFromDate.Value;
                return timeSpan;
            }

            return null;
        }

        public static TimeSpan? GetDiffStringDate(string fromdate, string todate)
        {
            var dtFromDate = ValidateDateString(fromdate);
            var dtToDate = CLDateTools.ValidateDateString(todate);
            if (dtFromDate.HasValue && dtToDate.HasValue)
            {
                TimeSpan timeSpan = dtToDate.Value - dtFromDate.Value;
                return timeSpan;
            }

            return null;
        }

        /// <summary>
        /// Given a date, this function returns the age according to the current date.
        /// </summary>
        /// <param name="givenDate"></param>
        /// <returns>The age if success otherwise -1</returns>
        /// <remarks>-1 is returned when parameter givenDate is null.</remarks>
        /// <seealso cref="DateTime"/>
        public static int GetAgeFromDate(DateTime givenDate)
        {
            if (givenDate == null)
            {
                return -1;
            }

            var dateNow = DateTime.Now;
            var age = dateNow.Year - givenDate.Year;

            if (dateNow.Month < givenDate.Month || (dateNow.Month == givenDate.Month && dateNow.Day < givenDate.Day))
                age--;

            return age;
        }

        public static bool ValidateYearString(string yearToValidate)
        {
            if (string.IsNullOrEmpty(yearToValidate))
            {
                return false;
            }

            DateTime dateTimeOut;
            return DateTime.TryParseExact(yearToValidate, "yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTimeOut);
        }

        /// <summary>
        /// Validate a date given a string parameter that must be conformed to a format.
        /// </summary>
        /// <param name="dateToValidate">The string to validate.</param>
        /// <returns>dateTime is the date is valid, otherwise null.</returns>
        /// <remarks>
        /// null is eturned when parameter dateToValidate is null or empty, <see cref="DateTime.TryParseExact"/> returns false.
        /// </remarks>
        public static DateTime? ValidateDateString(string dateToValidate)
        {
            DateTime dateTimeOut;

            if (string.IsNullOrEmpty(dateToValidate) || string.IsNullOrWhiteSpace(dateToValidate))
            {
                return null;
            }

            if (!DateTime.TryParseExact(dateToValidate, Formats, null, DateTimeStyles.None, out dateTimeOut))
            {
                return null;
            }

            DateTime? objDataTime = dateTimeOut;
            return objDataTime;
        }
    }
}
