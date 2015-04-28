using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltiFinReact.Core.Framework.Extensions.Strings
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces last occurence of string oldValue inside of this string with string newValue
        /// </summary>
        /// <param name="s">String for which replacement should be done</param>
        /// <param name="oldValue">Value to replace</param>
        /// <param name="newValue">Replacement value</param>
        /// <returns>Result of replacement</returns>
        public static string ReplaceLast(this string s, string oldValue, string newValue)
        {
            var i = s.LastIndexOf(oldValue);

            return i < 0 ? s : s.Remove(i, oldValue.Length).Insert(i, newValue);
        }


        public static string GetDocumentCode(this string pattern, string projectCode, int documentNumber, int year, string templateName = "", string documentName = "", int docNumberMinLength = 1)
        {
            var documentNumberTag = "<DocNr";
            var yearTag = "<Year>";
            var index = pattern.IndexOf(documentNumberTag);


            if (index != -1 && pattern.IndexOf('>', index) != -1)
            {
                var lastIndex = pattern.IndexOf('>', index);
                var strDocumentNumberDecimals = pattern.Substring(index + documentNumberTag.Length,
                                                        lastIndex - index - documentNumberTag.Length);

                var documentNumberDecimals = string.IsNullOrEmpty(strDocumentNumberDecimals) ? docNumberMinLength :
                    Int32.Parse(strDocumentNumberDecimals);

                var documentNumberFormat = "{0:";

                for (var i = 0; i < documentNumberDecimals; i++)
                    documentNumberFormat += '0';

                documentNumberFormat += '}';
                pattern = pattern.Replace(documentNumberTag + strDocumentNumberDecimals + '>',
                                          string.Format(documentNumberFormat, documentNumber));
            }

            return pattern
                .Replace(yearTag, (year % 100).ToString())
                .Replace("<ProjectCode>", projectCode)
                .Replace("<TemplateName>", templateName)
                .Replace("<DocumentName>", documentName);
        }
    }
}
