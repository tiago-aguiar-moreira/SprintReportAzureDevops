using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SprintReport.Console.Page
{
    public class BoardsPage
    {
        protected static WebDriverWait _webDriverWait;

        public BoardsPage(WebDriverWait webDriverWait) => _webDriverWait = webDriverWait;

        public SprintPage SprintPage()
        {
            var showMoreInfo = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("project-expansion-toggle")));

            if(showMoreInfo.GetAttribute("aria-label").Equals("Show more information"))
                showMoreInfo.Click();

            _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("__bolt-ms-vss-work-web-sprints-hub-link"))).Click();

            return new SprintPage(_webDriverWait);
        }
    }
}