using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SprintReport.Console.Page
{
    public class HomePage
    {
        protected static WebDriverWait _webDriverWait;

        public HomePage(WebDriverWait webDriverWait) => _webDriverWait = webDriverWait;

        public BoardsPage BoardPage(string projectName)
        {
            var tableProjList = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("project-list")));

            foreach(var item in tableProjList.FindElements(By.ClassName("project-row")))
            {
                if(item.FindElement(By.ClassName("project-name")).Text.Equals(projectName))
                {
                    item.FindElement(By.ClassName("product")).Click();
                    
                    return new BoardsPage(_webDriverWait);
                }
            }

            return null;
        }
    }
}