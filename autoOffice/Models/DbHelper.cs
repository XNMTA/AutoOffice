using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace autoOffice.Models
{
    public class DbHelper:DbContext
    {
        public DbHelper()
            : base("rcoa")
        {
            Database.SetInitializer<DbHelper>(new MigrateDatabaseToLatestVersion<DbHelper, ReportingDbMigrationsConfiguration>());
        }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
    public sealed class ReportingDbMigrationsConfiguration : DbMigrationsConfiguration<DbHelper>
    {
        public ReportingDbMigrationsConfiguration()
        {
            //all change on model class will update DB
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}