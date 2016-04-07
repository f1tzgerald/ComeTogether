using ComeTogether.DAL.Entities;
using ComeTogether.DAL.EntityFramework;
using ComeTogether.DAL.Interfaces;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Repositories
{
    public class ToDoItemRepository : IToDoItem
    {
        private ComeTogetherContext _context;

        public ToDoItemRepository(ComeTogetherContext context)
        {
            _context = context;
        }

        public void AddToDoItem(int categoryId, TodoItem toDoitem)
        {
            var category = GetCategoryById(categoryId);
            category.ToDoItems.Add(toDoitem);
            _context.ToDoItems.Add(toDoitem);
        }

        public IEnumerable<TodoItem> GetDeletedToDoItems()
        {
            return _context.ToDoItems.Where(c => c.IsDeleted == true).OrderBy(c => c.DateFinish).ToList();
        }

        public IEnumerable<TodoItem> GetToDoItemsForCategory(int categoryId)
        {
            var query = (from cat in _context.Category
                         where cat.Id == categoryId
                         select cat).Include(c => c.ToDoItems).FirstOrDefault();
            var tasks = query.ToDoItems.Where(c => c.IsDeleted == false);
            return tasks;
        }

        public IEnumerable<Comment> GetCommentsForToDoItem(int id)
        {
            var commentsInTasks = (from s in _context.ToDoItems
                                   where s.Id == id
                                   select s).Single().Comments.ToList();
            return commentsInTasks;
        }

        public TodoItem GetToDoItemById(int id)
        {
            return _context.ToDoItems.Include(c => c.Comments).Where(c => c.Id == id).FirstOrDefault();
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
            currentToDoItem.IsDeleted = toDoitem.IsDeleted;
            //currentToDoItem = toDoitem;
        }

        public void DeleteAllDeletedItems()
        {
            var deletedTasks = _context.ToDoItems.Where(c => c.IsDeleted == true).ToList();
            _context.RemoveRange(deletedTasks);
        }

        public void DeleteToDoItem(int taskId)
        {
            var taskToDelete = _context.ToDoItems.Where(c => c.Id == taskId).FirstOrDefault();
            _context.Remove(taskToDelete);
        }

        public void MoveAllDoneItemsToRecycle(int categoryId)
        {
            var category = _context.Category.Where(c => c.Id == categoryId).Include(c => c.ToDoItems).FirstOrDefault();
            var tasksToRecycle = category.ToDoItems.Where(c => c.IsDeleted == false && c.Done == true).ToList();

#warning 0 or null
            if (tasksToRecycle.Count == 0)
                return;

            foreach (var task in tasksToRecycle)
                task.IsDeleted = true;
        }
    }
}
