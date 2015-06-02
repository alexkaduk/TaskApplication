using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskApplication.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }

        // public virtual int IssueId { get; set; }
        // public virtual int SubTaskId { get; set; }
    }
}