using System.ComponentModel;

namespace SprintReport.Console.Model.Enums
{
    public enum EmailHostEnum
    {
        [Description("smtp.gmail.com")]
        Gmail = 1,
        [Description("smtp.live.com")]
        Hotmail = 2
    }
}