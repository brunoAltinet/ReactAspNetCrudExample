using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace AltiFinReact.Core.Framework
{
    public static class Utilities
    {

        #region Private Fields

        #endregion

        #region Properties

        public static string ApplicationFolderPath
        {
            get
            {
                var path = Path.Combine(Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "Altinet"), "AltiFinReact.Core.Framework");

                var dir = new DirectoryInfo(path);
                if (!dir.Exists)
                    dir.Create();

                return path;
            }
        }

        /// <summary>
        /// Gets currently logged in user's identity
        /// </summary>
        public static IIdentity CurrentUser
        {
            get
            {
                return Thread.CurrentPrincipal.Identity;
            }
        }


        #endregion

        #region Public Methods

        public static bool AreDatesSame(DateTime date1, DateTime date2)
        {
            if (date1.Year != date2.Year)
                return false;

            if (date1.Month != date2.Month)
                return false;

            if (date1.Day != date2.Day)
                return false;

            if (date1.Hour != date2.Hour)
                return false;

            if (date1.Minute != date2.Minute)
                return false;

            if (date1.Second != date2.Second)
                return false;

            // we'll disregard miliseconds and ticks
            return true;
        }

        /// <summary>
        /// Calculates interest based on principle, payment deadline and the actual payment date
        /// </summary>
        /// <param name="principle">Principle - the value that needs to be paid</param>
        /// <param name="deadline">Deadline for principle payment</param>
        /// <param name="datePaid">Actual date of principle payment</param>
        /// <returns></returns>
        public static decimal? CalculateInterest(decimal? principle, DateTime? deadline, DateTime? datePaid)
        {
            if (!principle.HasValue || !deadline.HasValue || !datePaid.HasValue || datePaid.Value <= deadline.Value)
                return null;

            var current = new DateTime(deadline.Value.Year, deadline.Value.Month, deadline.Value.Day);
            var paid = new DateTime(datePaid.Value.Year, datePaid.Value.Month, datePaid.Value.Day);

            if (paid <= current)
                return null; // if parameters had different time but the same date

            decimal interest = 0;

            while (current.Year <= paid.Year)
            {
                var days = current.Year < paid.Year ?
                    (new DateTime(current.Year, 12, 31) - current).Days :
                    (paid - current).Days;
                var divider = DateTime.IsLeapYear(current.Year) ? 36600 : 36500; // days in year * 100

                interest += principle.Value * 0.3m * days / divider;

                current = new DateTime(current.Year + 1, 1, 1);
            }

            return Math.Round(interest, 2);
        }

        /// <summary>
        /// Combines path list with separator character between them without making double separator characters
        /// </summary>
        /// <param name="separator">Character to use as a separator</param>
        /// <param name="paths">Comma delimited list of path parts to combine in one path</param>
        /// <returns>Path combined from provided path parts delimited with single separator character</returns>
        public static string Combine(char separator, params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return String.Empty;

            var result = paths[0];

            for (var i = 1; i < paths.Length; i++)
            {
                if (String.IsNullOrEmpty(paths[i]))
                    continue;

                var resultEndsWithSeparator = result.Length > 0 ? result[result.Length - 1] == separator : true;
                var pathStartsWithSeparator = paths[i].Length > 0 ? paths[i][0] == separator : true;

                if (!resultEndsWithSeparator && !pathStartsWithSeparator)
                    result += separator + paths[i];
                else if ((resultEndsWithSeparator && !pathStartsWithSeparator) || (!resultEndsWithSeparator))
                    result += paths[i];
                else result += paths[i].Substring(1);
            }

            return result;
        }


        /// <summary>
        /// Function to get byte array from a file
        /// </summary>
        /// <param name="fileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        public static byte[] FileToByteArray(string fileName)
        {
            byte[] buffer = null;

            try
            {
                // Open file for reading
                BinaryReader binaryReader;
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    binaryReader = new BinaryReader(fileStream);

                    // get total byte length of the file
                    long totalBytes = new FileInfo(fileName).Length;

                    // read entire file into buffer
                    buffer = binaryReader.ReadBytes((Int32)totalBytes);

                    // close file reader
                    fileStream.Close();
                    fileStream.Dispose();
                }
                binaryReader.Close();
            }
            catch (Exception exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", exception);
            }

            return buffer;
        }


        /// <summary>
        /// Searches for string value in provided string values
        /// </summary>
        /// <param name="s">String to search</param>
        /// <param name="values">String values in which current string is to be searched</param>
        /// <returns>True if string is found</returns>
        public static bool In<T>(this T s, params T[] values)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            return s != null && values != null && values.Any(x => s.Equals(x));
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        /// <summary>
        /// Checks whether provided OIB is valid
        /// </summary>
        /// <param name="oib">OIB to check for validity</param>
        /// <returns>True if OIB is valid, false otherwise</returns>
        public static bool IsOibValid(string oib)
        {
            if (oib.Length != 11) return false;

            long digits;
            if (!Int64.TryParse(oib, out digits)) return false;

            var check = 10L;

            for (var divider = 10000000000; divider >= 10; divider /= 10)
            {
                check += digits / divider;
                check %= 10;
                if (check == 0) check = 10;
                check *= 2;
                check %= 11;

                digits %= divider;
            }

            var control = 11 - check;

            if (control == 10)
                control = 0;

            return control == digits;
        }





        #endregion

    }
}
