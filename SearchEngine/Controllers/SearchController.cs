using SearchEngine.Models;
using SearchEngine.Service;
using System.Web.Mvc;

namespace SearchEngine.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "SearchText,SearchUrl")]SearchInput searchInput)
        {
            if (searchInput == null || !ModelState.IsValid)
            {
                return View();
            }

            searchInput.SearchResult = _searchService.SearchUrls(searchInput);
            return View(searchInput);
        }
    }
}