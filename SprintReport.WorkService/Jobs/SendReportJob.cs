using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SprintReport.WorkService.Jobs
{
    public class SendReportJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await File.AppendAllLinesAsync(@"c:\temp\job1.txt", new[] { DateTime.Now.ToLongTimeString() });
        }
    }
}