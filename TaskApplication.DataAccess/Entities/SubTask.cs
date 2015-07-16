using System.ComponentModel.DataAnnotations;

namespace TaskApplication.DataAccess.Entities
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
        public virtual Issue Issue { get; set; }

        // Foreign key 
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        // Navigation property
        public virtual Status Status { get; set; }
    }
}