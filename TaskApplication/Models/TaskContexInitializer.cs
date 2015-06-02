using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskApplication.Models
{
    public class TaskContexInitializer : DropCreateDatabaseIfModelChanges<TaskContex> // DropCreateDatabaseAlways<TaskContex> //
    {
        protected override void Seed(TaskContex contex)
        {
            contex.Statuses.Add(new Status { StatusName = "Open" });
            contex.Statuses.Add(new Status { StatusName = "Resolve" });

            contex.Categories.Add(new Category { CategoryName="Task", CategoryDescription = "Issue task category."});
            contex.Categories.Add(new Category { CategoryName = "Bag", CategoryDescription = "Issue bag category." });

            contex.SaveChanges();
            base.Seed(contex);
        }
    }
}