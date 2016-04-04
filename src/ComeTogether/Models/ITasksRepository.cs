using System.Collections.Generic;

namespace ComeTogether.Models
{
    public interface ITasksRepository : ICategory, IToDoItem, IComment, IUser
    {
        bool SaveChanges();
    }

    public interface ICategory
    {
        IEnumerable<Category> GetAllCategoriesForUser(string userId);
        IEnumerable<Category> GetAllCategoriesWithToDoItems();
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void EditCategory(int categoryId, Category editCategory);
        void DeleteCategory(int categoryId);
    }

    public interface IUser
    {
        IEnumerable<Person> GetAllUsersForCategory(int categoryId);
        IEnumerable<Person> GetAllUsers();
        Person GetUserByName(string userName);
    }

    public interface IToDoItem
    {
        void AddToDoItem(int categoryId, TodoItem toDoitem);
        TodoItem GetToDoItemById(int id);
        //IEnumerable<TodoItem> GetToDoItemsForCategory(int categoryId);
        void EditToDoItem(int todoitemId, TodoItem toDoitem);



        void DeleteToDoItem(int taskId);
        void DeleteAllDoneItems(int categoryId);
    }

    public interface IComment
    {
        void AddComment(int toDoItemId, Comment comment);
        //IEnumerable<Comment> GetCommentsForToDoItem(int id);
        Comment GetCommentById(int commentID);
        void DeleteComment(int commentId);
    }
}