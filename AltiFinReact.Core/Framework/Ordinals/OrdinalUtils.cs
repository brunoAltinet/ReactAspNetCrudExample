using System;
using System.Collections.Generic;

namespace Altinet.Ordinals
{
    public static class OrdinalUtils
    {
        private static OrdinalComparer instance;

        /// <summary>
        /// Compares ordinals
        /// </summary>
        /// <param name="ordinal1">First ordinal to compare</param>
        /// <param name="ordinal2">Second ordinal to compare</param>
        /// <param name="reverse"></param>
        /// <returns>Result of comparison</returns>
        public static int CompareOrdinals(object ordinal1, object ordinal2, bool reverse = false)
        {
            if (ordinal1 == null && ordinal2 == null)
                return 0;

            if (ordinal1 == null)
                return -1;

            if (ordinal2 == null)
                return 1;

            if (ordinal1.Equals(ordinal2))
                return 0;

            int i;
            var list1 = ordinal1.ToString().Split('.', '/', '-');
            var list2 = ordinal2.ToString().Split('.', '/', '-');
            if (reverse)
            {
                Array.Reverse(list1);
                Array.Reverse(list2);
            }

            for (i = 0; i < list1.Length && i < list2.Length; i++)
                if (!list1[i].Equals(list2[i]))
                    break;

            if (i >= list1.Length)
                return -1;

            if (i >= list2.Length)
                return 1;

            int value1, value2;

            if (!Int32.TryParse(list1[i], out value1) ||
                !Int32.TryParse(list2[i], out value2))
                return list1[i].CompareTo(list2[i]);

            return value1.CompareTo(value2);
        }

        public static OrdinalComparer Comparer
        {
            get { return instance ?? (instance = new OrdinalComparer()); }
        }

        public static string GetNextOrdinal(string lastOrdinal, string parentOrdinal=null)
        {
            if (lastOrdinal != null && lastOrdinal.Trim() == "")
                lastOrdinal = null;
            if (parentOrdinal != null && parentOrdinal.Trim() == "")
                parentOrdinal = null;

            if (parentOrdinal != null && lastOrdinal == parentOrdinal)
                lastOrdinal = null;
            var prefix = lastOrdinal != null ? GetParentOrdinal(lastOrdinal)+"." : CheckLastDot(parentOrdinal);
            int lastNum;
            if (!int.TryParse(prefix.GetLastMember(), out lastNum) && (lastOrdinal == null || !lastOrdinal.StartsWith(prefix)))
                prefix = ""; //ako zadnji clan prefiksa nije broj, onda napusta brojcanu notaciju i krece ispocetka
            string lastMember = !string.IsNullOrEmpty(lastOrdinal) ? GetLastMember(lastOrdinal) : "0";

            if (int.TryParse(lastMember, out lastNum))
                return prefix + (++lastNum);
            return prefix + (lastMember.Length > 0 ? ((char)(lastMember[lastMember.Length - 1] + 1)) : '0');
        }

        public static string CheckLastDot(this string ordinal)
        {
            if (ordinal == null)
                return "";
            var ord = ordinal.Trim();
            return ord.EndsWith(".") ? ord : ord + ".";
        }

        public static string GetLastMember(this string ordinal)
        {
            if (ordinal == null)
                return "";
            ordinal = ordinal.Trim();
            if (ordinal.EndsWith("."))
                ordinal = ordinal.Remove(ordinal.Length - 1);
            return ordinal.Contains(".") ? ordinal.Substring(ordinal.LastIndexOf(".") + 1) : ordinal;
        }

        public static string GetParentOrdinal(this string ordinal)
        {
            if (ordinal == null)
                return "";
            ordinal = ordinal.Trim();
            if (ordinal.EndsWith("."))
                ordinal = ordinal.Remove(ordinal.Length - 1);
            return ordinal.Contains(".") ? ordinal.Remove(ordinal.LastIndexOf(".")) : "";
        }

        public static string GetLastOrdinal(this IEnumerable<string> ordinals)
        {
            string max = null;
            foreach (var ord in ordinals)
            {
                if (CompareOrdinals(ord, max) > 0)
                    max = ord;
            }
            return max;
        }
    }
}
