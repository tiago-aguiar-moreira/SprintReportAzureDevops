using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SprintReport.Console.Data;
using SprintReport.Console.Model;
using SprintReport.Console.Page;
using System;
using System.Collections.Generic;

namespace SprintReport.Console.Business
{
    public class SprintReportBusiness
    {
        private readonly IConfigurationBuilder _configurationBuilder;

        public SprintReportBusiness(IConfigurationBuilder configurationBuilder)
            => _configurationBuilder = configurationBuilder;

        private SprintReportBusiness CreateReport()
        {
            
            return this;
        }

        public SprintReportBusiness SendReport()
        {
            
            return this;
        }

        public void TakeSnapshot()
        {
            IList<BacklogItemModel> backlogItems;
            IList<WorkProgressModel> workProgresses;

            var url = _configurationBuilder.Build().GetSection("url").Value;
            var userName = _configurationBuilder.Build().GetSection("userName").Value;
            var password = _configurationBuilder.Build().GetSection("password").Value;
            var boardName = _configurationBuilder.Build().GetSection("boardName").Value;

            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(url);

                new SingInPage(wait)
                    .ExecuteLogin(userName, password)
                    .BoardPage(boardName)
                    .SprintPage()
                    .Data(out backlogItems, out workProgresses);
            }

            using(var db = new SprintReportContext(_configurationBuilder))
            {
                foreach (var item in backlogItems)
                {
                    db.Add(item);
                }

                foreach (var item in workProgresses)
                {
                    db.Add(item);
                }

                db.SaveChanges();
            }
        }
    }
}