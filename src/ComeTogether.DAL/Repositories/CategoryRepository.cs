using ComeTogether.DAL.Entities;
using ComeTogether.DAL.EntityFramework;
using ComeTogether.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime;
using Microsoft.Data.Entity;

namespace ComeTogether.DAL.Repositories
{
    public class CategoryRepository : ICategory
    {
        private ComeTogetherContext _context;

        public CategoryRepository (ComeTogetherContext context)
        {
            _context = context;
        }
        
        public void AddCategory(Category category)
        {
            _context.Category.Add(category);
        }

        public void EditCategory(int categoryId, Category newCategory)
        {
            var currentCategory = _context.Category.Where(c => c.Id == categoryId).FirstOrDefault();
            currentCategory.Name = newCategory.Name;
        }

        public void DeleteCategory(int categoryId)
        {
            var categoryToDelete = _context.Category.Where(c => c.Id == categoryId).FirstOrDefault();
            _context.Category.Remove(categoryToDelete);
        }

        public IEnumerable<Category> GetAllCategoriesForUser(string userId)
        {
            var s = _context.Category.Where(c => c.CategoryPeople.Any(ac => ac.UserId == userId)).OrderBy(c => c.Name).ToList();
            return s;
        }

        public IEnumerable<Category> GetAllCategoriesWithToDoItems()
        {
            return _context.Category.Include(c => c.ToDoItems).OrderBy(c => c.Name).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            var ss = _context.Category.Include(c => c.ToDoItems).Where(c => c.Id == categoryId).FirstOrDefault();
            return ss;
        }

        public IEnumerable<Person> GetAllUsersForCategory(int categoryId)
        {
            return _context.CategoryPeople.Where(c => c.CategoryId == categoryId).Select(c => c.Person).ToList();
        }
    }
}
