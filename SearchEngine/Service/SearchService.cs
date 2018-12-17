using SearchEngine.Helper;
using SearchEngine.Models;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace SearchEngine.Service
{
    public interface ISearchService
    {
        string SearchUrls(SearchInput searchInput);
    }

    public class SearchService : ISearchService
    {
        public string SearchUrls(SearchInput searchInput)
        {
            Uri searchUri = new Uri($"https://{searchInput.SearchUrl}/");
            string searchUrl = "https://www.google.com/search?num=100&q={0}&btnG=Search";
            string requestString = string.Format(searchUrl, HttpUtility.UrlEncode(searchInput.SearchText));

            WebRequest request = WebRequest.Create(requestString);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer =  reader.ReadToEnd();
                    reader.Close();
                    return HtmlParser.FindAllHrefMatches(responseFromServer, searchUri);
                }
            }            
        }
    }
}
