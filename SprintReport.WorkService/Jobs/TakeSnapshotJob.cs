using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SprintReport.WorkService.Jobs
{
    public class TakeSnapshotJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await File.AppendAllLinesAsync(@"c:\temp\job2.txt", new[] { DateTime.Now.ToLongTimeString() });
        }
    }
}