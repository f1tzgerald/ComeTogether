using System;
using System.Collections.Generic;

namespace ComeTogether.Droid
{ 
    /// <summary>
    /// Manager classes are an abstraction on the data access layers
    /// </summary>
public static class TodoItemManager 
	{
		static TodoItemManager ()
		{
		}

        #region Tasks
        public static TodoItem GetTask(int id)
		{
			return TodoItemRepositoryADO.GetTask(id);
		}
		
		public static IList<TodoItem> GetTasks (int categoryId)
		{
			return new List<TodoItem>(TodoItemRepositoryADO.GetTasks(categoryId));
		}
		
		public static int SaveTask (TodoItem item)
		{
			return TodoItemRepositoryADO.SaveTask(item);
		}
		
		public static int DeleteTask(int id)
		{
			return TodoItemRepositoryADO.DeleteTask(id);
		}
        #endregion

        #region Category
        public static Category GetCategory(int id)
        {
            return TodoItemRepositoryADO.GetCategory(id);
        }

        public static IList<Category> GetCategories ()
        {
            return new List<Category>(TodoItemRepositoryADO.GetCategories());
        }

        public static int SaveCategory (Category item)
        {
            return TodoItemRepositoryADO.SaveCategory(item);
        }

        public static int DeleteCategory (int id)
        {
            return TodoItemRepositoryADO.DeleteCategory(id);
        }
        #endregion

        #region Comments

        public static IList<Comment> GetComments (int taskId)
        {
            return new List<Comment> (TodoItemRepositoryADO.GetComments(taskId));
        }

        public static int AddNewComment (Comment item)
        {
            return TodoItemRepositoryADO.AddNewComment(item);
        }
        public static int DeleteComment (int id)
        {
            return TodoItemRepositoryADO.DeleteComment(id);
        }
        #endregion
    }
}