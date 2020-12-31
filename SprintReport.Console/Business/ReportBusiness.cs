using Mustache;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SprintReport.Console.Business
{
    public class Report
    {
        public void Render(string titleChart, string labels, string[] currentWork, string[] currentCapacity)
        {
            var obj = new 
            {
                title = "Titulo da pagina",
                labels = string.Join(",", new string[] {"07:30", "08:00", "08:30"}.Select(s => string.Format("'{0}'", s))),
                currentWork = string.Join(",", new int[] {5, 4, 7}),
                currentCapacity = string.Join(",", new int[] {5, 8, 2})
            };

            using (StreamReader streamReader = new StreamReader(@"C:\Users\tiago.Moreira\source\repos\SprintReport\SprintReport.Console\Report\Template.txt", Encoding.UTF8))
            {
                FormatCompiler compiler = new FormatCompiler();
                Generator generator = compiler.Compile(streamReader.ReadToEnd());
                string result = generator.Render(obj);
                
                Debug.Print(result);
            }
        }
    }
}