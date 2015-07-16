using System.ComponentModel.DataAnnotations;

namespace TaskApplication.DataAccess.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }
    }
}