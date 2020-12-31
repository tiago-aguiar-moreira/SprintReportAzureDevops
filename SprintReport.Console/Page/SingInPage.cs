using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SprintReport.Console.Page
{
    public class SingInPage
    {
        protected static WebDriverWait _webDriverWait;

        public SingInPage(WebDriverWait webDriverWait)
            => _webDriverWait = webDriverWait;

        private void ClickButtonSingIn()
            => _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("idSIButton9"))).Click();

        private void SetUserName(string userName)
            => _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("i0116"))).SendKeys(userName);

        private void SetPassword(string password)
            => _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("i0118"))).SendKeys(password);

        private void ClickButtonNotStayConnected()
            => _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("idBtn_Back"))).Click();

        public HomePage ExecuteLogin(string userName, string password)
        {
            SetUserName(userName);
            ClickButtonSingIn();

            SetPassword(password);
            ClickButtonSingIn();

            ClickButtonNotStayConnected();

            return new HomePage(_webDriverWait);
        }
    }
}