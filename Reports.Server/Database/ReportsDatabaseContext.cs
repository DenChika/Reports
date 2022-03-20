using Microsoft.EntityFrameworkCore;
using Reports.DAL.Comments;
using Reports.DAL.Entities;
using Reports.DAL.TasksChange;

namespace Reports.Server.Database
{
    public class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<TaskChange> TasksChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Report>().ToTable("Reports");
            modelBuilder.Entity<TaskChange>().ToTable("TasksChanges");
            base.OnModelCreating(modelBuilder);
        }
    }
}