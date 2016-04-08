using System;
using System.Collections.Generic;

namespace ComeTogether.Droid
{
	/// <summary>
	/// Todo Item business object
	/// </summary>
	public class TodoItem 
	{
        public int ID { get; set; }
		public string Name { get; set; }
        public string DateAdded { get; set; }
        public string DateFinish { get; set; }
		public bool Done { get; set; }
        public int CategoryId { get; set; }
	}

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<People> People {get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public int ToDoItemId { get; set; }
        public string Creator { get; set; }
        public string DateAdded { get; set; }
        public string Text { get; set; }
    }

    public class People
    {
        public string Name { get; set; }
#warning Check bitmap
        //public Android.Graphics.Bitmap Avatar { get; set; }
    }
}