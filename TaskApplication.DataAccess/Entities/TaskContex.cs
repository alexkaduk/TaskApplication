using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Models
{
    public class TaskContex : DbContext
    {
        public DbSet<Issue> Issues { get; set; }

        public DbSet<SubTask> SubTasks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}