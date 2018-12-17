using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchEngine.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace SearchEngine.Tests.Helper
{
    [TestClass]
    public class HtmlParserTest
    {
        [TestMethod]
        public void HtmlParser_Test()
        {
            string html = File.ReadAllText(@"Sample Data\SampleResults.html");
            string results = HtmlParser.FindAllHrefMatches(html, new Uri("https://www.infotrack.com.au"));

            Assert.IsTrue(results != "0");
        }
    }
}
