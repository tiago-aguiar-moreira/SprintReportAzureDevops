using Microsoft.Extensions.Configuration;
using SprintReport.Console.Business;
using SprintReport.Console.Model.Enums;
using System;
using System.IO;

namespace SprintReport.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Report().Render();

            // var builder = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // if (args.Length == 0)
            // {
            //     throw new ArgumentException("Option not found");
            // }

            // if(!int.TryParse(args[0], out var execOption))
            // {
            //     throw new ArgumentException("Invalid option");
            // }

            // try
            // {
            //     switch ((ExecutionOption)execOption)
            //     {
            //         case ExecutionOption.Report:
            //             new SprintReportBusiness(builder).SendReport();
            //             break;
            //         case ExecutionOption.Snapshot:
            //             new SprintReportBusiness(builder).TakeSnapshot();
            //             break;
            //         default:
            //             throw new ArgumentException("Invalid option");
            //     }
            // }
            // catch (Exception ex)
            // {
            //     System.Console.Write(ex);
            // }
        }
    }
}