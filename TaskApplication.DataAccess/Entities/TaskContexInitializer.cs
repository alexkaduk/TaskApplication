using System.Data.Entity;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Models
{
    public class TaskContexInitializer : DropCreateDatabaseIfModelChanges<TaskContex> //DropCreateDatabaseAlways
    {
        protected override void Seed(TaskContex contex)
        {
            contex.Statuses.Add(new Status { StatusName = "Open", StatusId = (int)Statuses.Open});
            contex.Statuses.Add(new Status { StatusName = "Resolved", StatusId = (int)Statuses.Resolved });

            contex.Categories.Add(new Category { CategoryName="Task", CategoryDescription = "Issue task category."});
            contex.Categories.Add(new Category { CategoryName = "Bag", CategoryDescription = "Issue bag category." });

            contex.SaveChanges();
            base.Seed(contex);
        }
    }
}