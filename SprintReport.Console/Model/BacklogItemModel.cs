using System;

namespace SprintReport.Console.Model
{
    public class BacklogItemModel
    {
        public int Id { get; set; }
        public int NroOrder { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string AssignedTo { get; set; }
        public decimal Effort { get; set; }
        public decimal RemainingWork { get; set; }
        public DateTime CreateDate { get; set; }
        public string AreaPath { get; set; }

        public BacklogItemModel(int nroOrder, string number, string title, string state, string assignedTo, decimal effort, decimal remainingWork, DateTime createDate, string areaPath)
        {
            NroOrder = nroOrder;
            Number = number;
            Title = title;
            State = state;
            AssignedTo = assignedTo;
            Effort = effort;
            RemainingWork = remainingWork;
            CreateDate = createDate;
            AreaPath = areaPath;
        }
    }
}