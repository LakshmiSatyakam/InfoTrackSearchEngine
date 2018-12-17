using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SearchEngine.Models
{
    /// <summary>
    /// Model class for Search input
    /// </summary>
    [Bind(Include = "SearchText,SearchUrl")]
    public class SearchInput
    {
        [Display(Name = "Search Text")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter SearchText")]
        public string SearchText { get; set; }

        [Display(Name = "Search Url")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Enter SearchUrl")]
        public string SearchUrl { get; set; }

        /// <summary>
        /// Search result string
        /// </summary>
        [Display(Name = "Search Result")]
        public string SearchResult { get; set; }
    }
}