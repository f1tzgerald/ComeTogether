using System.Collections.Generic;

namespace ComeTogether.Models
{
    public interface ITasksRepository
    {
        void AddComment(int toDoItemId, Comment comment);
        void AddToDoItem(string categoryName, TodoItem toDoitem);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Category> GetAllCategoriesWithToDoItems();
        Category GetCategoryByName(string name);
        IEnumerable<Comment> GetCommentsForToDoItem(int id);
        TodoItem GetToDoItemById(int id);
        IEnumerable<TodoItem> GetToDoItemsForCategory(string categoryName);
        void AddCategory(Category category);
        void EditCategory(string categoryName, Category editCategory);
        bool SaveChanges();
    }
}