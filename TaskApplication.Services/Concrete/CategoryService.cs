using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using TaskApplication.Common;
using TaskApplication.DataAccess.Entities;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.Services.Interfaces;

namespace TaskApplication.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(CategoryService));
        
        //private CategoryRepository _categoryReposiltory = new CategoryRepository();
        //private IssueRepository _issueReposiltory = new IssueRepository();

        private readonly ICategoryRepository _categoryReposiltory = Ioc.Get<ICategoryRepository>();
        private readonly IIssueRepository _issueReposiltory = Ioc.Get<IIssueRepository>();

        public IEnumerable<Category> GetAll()
        {
            try
            {
                return _categoryReposiltory.GetAll();
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
                return _categoryReposiltory.FindSingleBy(c => c.CategoryId == id);
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
                _categoryReposiltory.Add(category);
                _categoryReposiltory.Save();
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
                _categoryReposiltory.Edit(category);
                _categoryReposiltory.Save();
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
                    _categoryReposiltory.Delete(category);
                    _categoryReposiltory.Save();
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
                return _issueReposiltory.FindBy(i => i.CategoryId == id).Any();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
            
        }
    }
}
