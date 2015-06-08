using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.Services.Interfaces;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private CategoryReposiltory categoryReposiltory = new CategoryReposiltory();
        private IssueReposiltory issueReposiltory = new IssueReposiltory();

        public IEnumerable<Category> GetAll()
        {
            return categoryReposiltory.GetAll();
        }

        public Category FindSingleBy(int id)
        {
            return categoryReposiltory.FindSingleBy(c => c.CategoryId == id);
        }

        public void Add(Category category)
        {
            categoryReposiltory.Add(category);
            categoryReposiltory.Save();
        }

        public void Edit(Category category)
        {
            categoryReposiltory.Edit(category);
            categoryReposiltory.Save();
        }

        public void Delete(int id)
        {
            Category category = FindSingleBy(id);
            if (category != null)
            {
                categoryReposiltory.Delete(category);
                categoryReposiltory.Save();
            }
        }

        public bool IsUsed(int id)
        {
            return issueReposiltory.FindBy(i => i.CategoryId == id).Any();
        }
    }
}
