using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskApplication.DataAccess.Entities
{
    public class Issue
    {
        public int IssueId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string IssueName { get; set; }

        [Display(Name = "Description")]
        public string IssueDescription { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueCreateDate { get; set; }

        [Display(Name = "Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueUpdateDate { get; set; }

        // Foreign key 
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        // Foreign key 
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation properties 
        public virtual Status Status { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<SubTask> SubTasks { get; set; }
    }
}