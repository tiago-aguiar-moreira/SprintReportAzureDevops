using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SprintReport.Console.Model;

namespace SprintReport.Console.Data
{
    public class SprintReportContext : DbContext
    {
        private readonly string connectionString;
        public DbSet<BacklogItemModel> BacklogItem { get; set; }
        public DbSet<WorkProgressModel> WorkProgress { get; set; }
        
        public SprintReportContext(IConfigurationBuilder configurationBuilder) : base()
            => this.connectionString = configurationBuilder.Build().GetSection("connectionString").Value;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(connectionString);
    }
}