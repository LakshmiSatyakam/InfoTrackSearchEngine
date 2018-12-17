using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SearchEngine.Controllers;
using SearchEngine.Models;
using SearchEngine.Service;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SearchEngine.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTest
    {
        private SearchController _controller;
        private Mock<ISearchService> _mockService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockService = new Mock<ISearchService>();
            _controller = new SearchController(_mockService.Object);
        }

        [TestMethod]
        public void Index()
        {
            ViewResult result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DoSearch_Null_Test()
        {
            ActionResult result = _controller.Index(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DoSearch_Invalid_Input1_Test()
        {
            SearchInput searchInput = new SearchInput { SearchText = string.Empty, SearchUrl = string.Empty };
            ValidateViewModel(_controller, searchInput);
            ActionResult result = _controller.Index(searchInput);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(searchInput.SearchResult);
        }

        [TestMethod]
        public void DoSearch_Invalid_Input2_Test()
        {
            SearchInput searchInput = new SearchInput { SearchText = "Online title search", SearchUrl = string.Empty };
            ValidateViewModel(_controller, searchInput);
            ActionResult result = _controller.Index(searchInput);

            Assert.IsNotNull(result);
            Assert.IsNull(searchInput.SearchResult);
        }

        [TestMethod]
        public void DoSearch_Invalid_Input3_Test()
        {
            SearchInput searchInput = new SearchInput { SearchText = string.Empty, SearchUrl = "www.infotrack.com.au" };
            ValidateViewModel(_controller, searchInput);
            ActionResult result = _controller.Index(searchInput);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(searchInput.SearchResult);
        }

        [TestMethod]
        public void DoSearch_Test()
        {
            SearchInput searchInput = new SearchInput { SearchText = "Online title search", SearchUrl = "www.infotrack.com.au" };
            _mockService.Setup(x => x.SearchUrls(searchInput)).Returns(string.Empty);
        
            ActionResult result = _controller.Index(searchInput);

            Assert.IsNotNull(result);
        }

        private void ValidateViewModel(Controller controller, object input)                                                            
        {
            var validationContext = new ValidationContext(input, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(input, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }
        }
    }
}
