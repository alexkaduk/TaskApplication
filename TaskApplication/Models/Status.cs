using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskApplication.Models
{
    public class Status
    {
        public int StatusId { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        // public virtual int IssueId { get; set; }
        // public virtual int SubTaskId { get; set; }
    }
}