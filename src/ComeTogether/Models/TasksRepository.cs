using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Models
{
    public class TasksRepository : ITasksRepository
    {
        private MainContextDb _context;

        public TasksRepository(MainContextDb context)
        {
            _context = context;
        }

        #region ADD To repository
        public void AddComment(int toDoItemId, Comment comment)
        {
            var toDoItem = GetToDoItemById(toDoItemId);
            toDoItem.Comments.Add(comment);
            _context.Comments.Add(comment);
        }

        public void AddToDoItem(int categoryId, TodoItem toDoitem)
        {
            var category = GetCategoryById(categoryId);
            category.ToDoItems.Add(toDoitem);
            _context.ToDoItems.Add(toDoitem);
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
#warning Cascade delete
            var categoryToDelete = _context.Category.Where(c => c.Id == categoryId).FirstOrDefault();
            _context.Category.Remove(categoryToDelete);
        }
        #endregion

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Category.OrderBy(c=>c.Name).ToList();
        }

        public IEnumerable<Category> GetAllCategoriesWithToDoItems()
        {
            return _context.Category.Include(c => c.ToDoItems).OrderBy(c => c.Name).ToList();
        }

        //public IEnumerable<TodoItem> GetDeletedToDoItems()
        //{
        //    return _context.ToDoItems.Where(c => c.isDeleted == true).OrderBy(c => c.DateFinish).ToList();
        //}

        public IEnumerable<TodoItem> GetToDoItemsForCategory(int categoryId)
        {
            var tasksInCategory = (from s in _context.Category
                                   where s.Id == categoryId
                                   select s).Single().ToDoItems.ToList();
            return tasksInCategory;
        }

        public IEnumerable<Comment> GetCommentsForToDoItem(int id)
        {
            var commentsInTasks = (from s in _context.ToDoItems
                                   where s.Id == id
                                   select s).Single().Comments.ToList();
            return commentsInTasks;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Category.Include(c => c.ToDoItems).Where(c => c.Id == categoryId).FirstOrDefault();
        }

        public TodoItem GetToDoItemById(int id)
        {
            return _context.ToDoItems.Include(c => c.Comments).Where(c => c.Id == id).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void EditToDoItem(int todoitemId, TodoItem toDoitem)
        {
            var currentToDoItem = _context.ToDoItems.Where(c => c.Id == todoitemId).FirstOrDefault();
            //#warning HARDCODING
            currentToDoItem.Comments = toDoitem.Comments;
            currentToDoItem.Creator = toDoitem.Creator;
            currentToDoItem.DateAdded = toDoitem.DateAdded;
            currentToDoItem.DateFinish = toDoitem.DateFinish;
            currentToDoItem.Done = toDoitem.Done;
            currentToDoItem.Name = toDoitem.Name;
            currentToDoItem.WhoDoIt = toDoitem.WhoDoIt;
            //currentToDoItem = toDoitem;
        }

        public void DeleteAllDoneItems(int categoryId)
        {
#warning Insert Cascade delete
            var category = _context.Category.Include(c => c.ToDoItems).Where(c => c.Id == categoryId).FirstOrDefault();
            var doneTasks = category.ToDoItems.Where(c => c.Done == true).ToList();
            _context.RemoveRange(doneTasks);
        }

        public void DeleteToDoItem (int taskId)
        {
#warning Insert Cascade delete
            var taskToDelete = _context.ToDoItems.Where(c => c.Id == taskId).FirstOrDefault();
            _context.Remove(taskToDelete);
        }

        public Comment GetCommentById (int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
            _context.Remove(comment);
        }
    }
}
