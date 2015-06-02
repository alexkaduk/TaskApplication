using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskApplication.Models
{
    public class SubTask
    {
        public int SubTaskId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string SubTaskName { get; set; }

        [Display(Name = "Descrioption")]
        public string SubTaskDescrioption { get; set; }

        // Foreign key
        [Display(Name = "Issue")]
        public int IssueId { get; set; }
        // public virtual Issue Issue { get; set; }

        // Foreign key 
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        // Navigation property
        public virtual Status Status { get; set; }




        //public virtual int IssueId { get; set; }
        /*
        определить связь один-ко-многим в классе является:
         * создание дочерней коллекции в одном классе 
         * и внешнего ключа вместе со свойством навигации (navigation property) в дочернем классе.
        */

        /* N to M
         * робимо дві колекції
         * class Post
         * public ICollection<Tag> Tags { get; set; }
         * 
         * class Tag 
         * public ICollection<Post> Posts { get; set; }
         * 
         
         */
    }
}