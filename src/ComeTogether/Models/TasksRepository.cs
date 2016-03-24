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

        public void AddToDoItem(string categoryName, TodoItem toDoitem)
        {
            var category = GetCategoryByName(categoryName);
            category.ToDoItems.Add(toDoitem);
            _context.ToDoItems.Add(toDoitem);
        }

        public void AddCategory(Category category)
        {
            _context.Category.Add(category);
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

        public IEnumerable<TodoItem> GetToDoItemsForCategory(string categoryName)
        {
            var tasksInCategory = (from s in _context.Category
                     where s.Name == categoryName
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

        public Category GetCategoryByName(string name)
        {
            return _context.Category.Include(c => c.ToDoItems).Where(c => c.Name == name).FirstOrDefault();
        }

        public TodoItem GetToDoItemById(int id)
        {
            return _context.ToDoItems.Include(c => c.Comments).Where(c => c.Id == id).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void EditCategory(string categoryName, Category newCategory)
        {
            var currentCategory = _context.Category.Where(c => c.Name == categoryName).FirstOrDefault();
            currentCategory.Name = newCategory.Name;
        }
    }
}
