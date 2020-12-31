using System;
using SprintReport.Console.Model.Enums;

namespace SprintReport.Console.Model
{
    public class WorkProgressModel
    {
        public int Id { get; set; }
        public WorkProgressTypeEnum WorkProgressType { get; set; }
        public string Name { get; set; }
        public string Capacity { get; set; }
        public string CurrentWork { get; set; }
        public DateTime CreateDate { get; set; }

        public WorkProgressModel(WorkProgressTypeEnum workProgressType, DateTime createDate)
        {
            WorkProgressType = workProgressType;
            CreateDate = createDate;
        }
    }
}