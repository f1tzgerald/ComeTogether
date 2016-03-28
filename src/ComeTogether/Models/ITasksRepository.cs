using System.Collections.Generic;

namespace ComeTogether.Models
{
    public interface ITasksRepository
    {
        void AddComment(int toDoItemId, Comment comment);
        void AddToDoItem(int categoryId, TodoItem toDoitem);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Category> GetAllCategoriesWithToDoItems();
        Category GetCategoryById(int categoryId);
        IEnumerable<Comment> GetCommentsForToDoItem(int id);
        TodoItem GetToDoItemById(int id);
        IEnumerable<TodoItem> GetToDoItemsForCategory(int categoryId);
        void AddCategory(Category category);
        void EditCategory(int categoryId, Category editCategory);
        bool SaveChanges();
        void DeleteCategory(int categoryId);
    }
}