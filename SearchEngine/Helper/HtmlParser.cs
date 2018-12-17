using System;
using System.Text.RegularExpressions;

namespace SearchEngine.Helper
{
    public class HtmlParser
    {
        /// <summary>
        /// Returns the list of indices found 
        /// </summary>
        /// <param name="html">html result</param>
        /// <param name="url">Link to be looked for</param>
        /// <returns></returns>
        public static string FindAllHrefMatches(string html, Uri url)
        {
            html = html.Substring(html.IndexOf("<div class=\"sd\" id=\"resultStats\">"));
            html = html.Substring(html.IndexOf("<div class=\"g\">"));
            string lookup = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
            MatchCollection matches = Regex.Matches(html, lookup);

            string result = string.Empty;

            for (int i = 0; i < matches.Count; i++)
            {
                string match = matches[i].Groups[0].Value;
                if (match.Contains(url.Host))
                {
                    result = result + (i + 1) + ",";
                }
            }

            return result == string.Empty ? "0" : result.TrimEnd(',');
        }
    }
}