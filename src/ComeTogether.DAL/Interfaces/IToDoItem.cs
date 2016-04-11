using ComeTogether.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Interfaces
{
    public interface IToDoItem
    {
        void AddToDoItem(int categoryId, TodoItem toDoitem);
        TodoItem GetToDoItemById(int id);
        IEnumerable<TodoItem> GetToDoItemsForCategory(int categoryId);
        void EditToDoItem(int todoitemId, TodoItem toDoitem);

        void MoveAllDoneItemsToRecycle(int categoryId);

        IEnumerable<TodoItem> GetDeletedToDoItems();

        void DeleteToDoItem(int taskId);
        void DeleteAllDeletedItems();
    }
}
