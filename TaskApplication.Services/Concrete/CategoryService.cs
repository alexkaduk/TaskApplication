using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.Services.Interfaces;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.DataAccess.Entities;
using log4net;

namespace TaskApplication.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(CategoryService));
        private CategoryReposiltory categoryReposiltory = new CategoryReposiltory();
        private IssueReposiltory issueReposiltory = new IssueReposiltory();

        public IEnumerable<Category> GetAll()
        {
            try
            {
                return categoryReposiltory.GetAll();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Category[0];
            }
        }

        public Category FindSingleBy(int id)
        {
            try
            {
                return categoryReposiltory.FindSingleBy(c => c.CategoryId == id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Category();
            }
        }

        public void Add(Category category)
        {
            try
            {
                categoryReposiltory.Add(category);
                categoryReposiltory.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public void Edit(Category category)
        {
            try
            {
                categoryReposiltory.Edit(category);
                categoryReposiltory.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Category category = FindSingleBy(id);
                if (category != null)
                {
                    categoryReposiltory.Delete(category);
                    categoryReposiltory.Save();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public bool IsUsed(int id)
        {
            try
            {
                return issueReposiltory.FindBy(i => i.CategoryId == id).Any();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
            
        }
    }
}
