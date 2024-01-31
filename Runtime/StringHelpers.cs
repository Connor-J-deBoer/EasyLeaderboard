using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Connor.Utils
{
    public static class StringHelpers
    {
        /// <summary>
        /// This guy shortens a string based on a char and a max length
        /// </summary>
        /// <param name="originalString">The name</param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string ShortenString(this string originalString, int maxLength = 10, char delimiter = '#')
        {
            int delimiterIndex = originalString.IndexOf(delimiter);
            string cleanName = delimiterIndex > -1 ? originalString.Substring(0, delimiterIndex) : originalString;

            // Shorten the name to the maxLength and append an ellipsis if it was longer
            if (cleanName.Length > maxLength)
            {
                cleanName = cleanName.Substring(0, maxLength) + "...";
            }

            return cleanName;
        }
    }
}
