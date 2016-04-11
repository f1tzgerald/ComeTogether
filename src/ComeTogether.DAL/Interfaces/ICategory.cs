using ComeTogether.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Interfaces
{
    public interface ICategory
    {
        IEnumerable<Category> GetAllCategoriesForUser(string userId);
        IEnumerable<Category> GetAllCategoriesWithToDoItems();
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void EditCategory(int categoryId, Category editCategory);
        void DeleteCategory(int categoryId);
    }
}
