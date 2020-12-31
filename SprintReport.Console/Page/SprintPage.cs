using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SprintReport.Console.Model;
using SprintReport.Console.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace SprintReport.Console.Page
{
    public class SprintPage
    {
        protected static WebDriverWait _webDriverWait;

        public SprintPage(WebDriverWait webDriverWait) => _webDriverWait = webDriverWait;

        public void Data(out IList<BacklogItemModel> backlogItem, out IList<WorkProgressModel> workProgress)
        {
            Prepare();
            backlogItem = GetBacklog();
            workProgress = GetWork();
        }
    
        #region Private Methods

        private IList<WorkProgressModel> GetWork()
        {
            var work = GetWorkMember();
            work.Add(GetWorkTeam());

            return work;
        }

        private IList<BacklogItemModel> GetBacklog()
        {
            // var divBacklogContainer = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("iteration-backlog-container")));

            // var fields = GetFields(divBacklogContainer.FindElements(By.ClassName("grid-header-column")));

            // var rows = divBacklogContainer.FindElements(By.ClassName("grid-row"));

            // foreach (var row in rows)
            // {
                
            // }

            return new List<BacklogItemModel>();
        }

        private IDictionary<string, int> GetFields(ReadOnlyCollection<IWebElement> headerElement)
        {
            var fields = new Dictionary<string, int>();
            
            for (int i = 0; i < headerElement.Count(); i++)
            {
                fields.Add(headerElement[i].GetAttribute("aria-label") ?? string.Empty, i);
            }

            return fields;
        }

        private void Prepare()
        {
            // Wait load page sprint
            _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@data-icon-name='sprint']")));

            // Show options
            var buttonViewOptions = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@aria-label='View options']")));
            buttonViewOptions.Click();

            // Get option 'Work details'
            var optionWorkDetail = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@name='Work details']")));

            if(bool.Parse(optionWorkDetail.GetAttribute("aria-checked") ?? "false")) // Check selected option
                buttonViewOptions.Click(); // Hide options
            else
                optionWorkDetail.Click(); // Select option 'Work details'

            // Verificar se a barra de filtrar está sendo exibida pelo botão do funil
        }

        private WorkProgressModel GetWorkTeam()
        {
            var panelWorkDetails = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("work-details-panel-container")));

            var divWork = panelWorkDetails.FindElement(By.ClassName("team-capacity-control"));

            return GetProgress(WorkProgressTypeEnum.Team, divWork);
        }

        private IList<WorkProgressModel> GetWorkMember()
        {
            var divWorkBy = _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("assigned-to-grouped-progress-control")));

            return divWorkBy.FindElements(By.ClassName("capacity-pane-progress-control"))
                .Select(s => GetProgress(WorkProgressTypeEnum.Member, s))
                .ToList();
        }

        private WorkProgressModel GetProgress(WorkProgressTypeEnum workProgressType, IWebElement webElement)
        {
            var workProgress = new WorkProgressModel(workProgressType, DateTime.UtcNow);
            switch (workProgressType)
            {
                case WorkProgressTypeEnum.Team:
                    workProgress.Name = webElement.FindElement(By.ClassName("display-text")).Text;
                    break;
                case WorkProgressTypeEnum.Member:
                    workProgress.Name = webElement.FindElement(By.ClassName("identity-picker-resolved-name")).Text;
                    break;
                default:
                    workProgress.Name = string.Empty;
                    break;
            }

            var hrs = webElement.FindElement(By.ClassName("progress-text")).Text.Split(' ');
            
            switch (hrs.Length)
            {
                case 2:
                    workProgress.CurrentWork = "0";
                    workProgress.Capacity = Regex.Replace(hrs[0], "[^0-9]", "");
                    break;
                case 4:
                    workProgress.CurrentWork = Regex.Replace(hrs[0], "[^0-9]", "");
                    workProgress.Capacity = hrs[2];
                    break;
                default:
                    workProgress.CurrentWork = "0";
                    workProgress.Capacity = "0";
                    break;
            }

            return workProgress;
        }
        
        #endregion
    }
}