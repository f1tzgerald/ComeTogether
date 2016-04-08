using System;
using System.Linq;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace ComeTogether.Droid
{
	/// <summary>
	/// TaskDatabase uses ADO.NET to create the [Items] table and create,read,update,delete data
	/// </summary>
	public class TodoDatabase 
	{
		static object locker = new object ();

		public SqliteConnection connection;

		public string path;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		public TodoDatabase (string dbPath) 
		{
			path = dbPath;
			// create the tables
			bool exists = File.Exists (dbPath);

			if (!exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);

                connection.Open();
                var commands = new[] 
                {
                    "CREATE TABLE [Category] (id INTEGER PRIMARY KEY ASC, Name NTEXT);",
                    "CREATE TABLE [Items] (_id INTEGER PRIMARY KEY ASC, Name NTEXT, DateAdded NTEXT, DateFinish NTEXT, Done INTEGER, Category_id INTEGER, FOREIGN KEY (Category_id) REFERENCES Category(id));",
                    "CREATE TABLE [Comments] (id INTEGER PRIMARY KEY ASC, Text NTEXT, Items_id INTEGER, Creator NTEXT, DateAdded NTEXT, FOREIGN KEY (Items_id) REFERENCES Items(id));"
                };

                foreach (var command in commands)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        var i = c.ExecuteNonQuery();
                    }
                }
                connection.Close();                
            }
        }

        #region Tasks
        /// <summary>Convert from DataReader to Task object</summary>
        private TodoItem FromReader (SqliteDataReader r)
        {
			TodoItem t = new TodoItem ();
			t.ID = Convert.ToInt32 (r ["_id"]);
			t.Name = r ["Name"].ToString ();
			t.DateAdded = r ["DateAdded"].ToString ();
            t.DateFinish = r["DateFinish"].ToString();
            t.Done = Convert.ToInt32 (r ["Done"]) == 1 ? true : false;
			return t;
		}

		public IEnumerable<TodoItem> GetTaskItems (int categoryId)
		{
			var tl = new List<TodoItem> ();

			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT [_id], [Name], [DateAdded], [DateFinish], [Done] from [Items] WHERE [Category_id] = ?";
                    contents.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = categoryId });
                    var r = contents.ExecuteReader();
                    while (r.Read())
                    { tl.Add(FromReader(r)); }
                }
				connection.Close ();
			}
			return tl;
		}

		public TodoItem GetTaskItem (int id) 
		{
			TodoItem t = new TodoItem ();
			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "SELECT [_id], [Name], [DateAdded], [DateFinish], [Done] from [Items] WHERE [_id] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
					var r = command.ExecuteReader ();
					while (r.Read ()) {
						t = FromReader (r);
						break;
					}
				}
				connection.Close ();
			}
			return t;
		}

		public int SaveTaskItem (TodoItem item)
		{
			int r;
			lock (locker) {
				if (item.ID != 0) {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE [Items] SET [Name] = ?, [DateAdded] = ?, [DateFinish] = ?, [Category_id] = ?, [Done] = ? WHERE [_id] = ?;";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Name });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.DateAdded });
                        command.Parameters.Add(new SqliteParameter(DbType.DateTime) { Value = item.DateFinish });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.CategoryId });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.Done });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.ID });
                        r = command.ExecuteNonQuery();
                    }
					connection.Close ();
					return r;
				}
                else {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [Items] ([Name], [DateAdded], [DateFinish], [Category_id], [Done]) VALUES (?, ?, ?, ?, ?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = DateTime.Now.ToShortDateString() });
                        command.Parameters.Add(new SqliteParameter(DbType.DateTime) { Value = item.DateFinish });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.CategoryId });
                        command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}
			}
		}

		public int DeleteTaskItem(int id) 
		{
			lock (locker) {
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Items] WHERE [_id] = ?;";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    r = command.ExecuteNonQuery();
                }
				connection.Close ();
				return r;
			}
		}
        #endregion

        #region Categories

        public IEnumerable<Category> GetCategoryItems()
        {
            var _categoriesList = new List<Category>();

            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT [id], [Name] from [Category]";
                    var r = contents.ExecuteReader();
                    while (r.Read())
                    {
                        Category t = new Category();
                        t.Id = Convert.ToInt32(r["id"]);
                        t.Name = r["Name"].ToString();
                        _categoriesList.Add(t);
                    }
                }
                connection.Close();
            }
            return _categoriesList;
        }

        public Category GetCategoryItem (int id)
        {
            Category cat = new Category();
            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [id], [Name] from [Category] WHERE [id] = ?";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    var r = command.ExecuteReader();
                    while (r.Read())
                    {
                        cat.Id = Convert.ToInt32(r["id"]);
                        cat.Name = r["Name"].ToString();
                        break;
                    }
                }
                connection.Close();
            }
            return cat;
        }

        public int SaveCategoryItem(Category item)
        {
            int r;
            lock (locker)
            {
                if (item.Id != 0)
                {
                    connection = new SqliteConnection("Data Source=" + path);
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE [Category] SET [Name] = ? WHERE [_id] = ?;";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Name });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.Id });
                        r = command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return r;
                }
                else {
                    connection = new SqliteConnection("Data Source=" + path);
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [Category] ([Name]) VALUES (?)";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Name });
                        r = command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return r;
                }
            }
        }

        public int DeleteCategoryItem(int id)
        {
            lock (locker)
            {
                int r;
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Category] WHERE [id] = ?;";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    r = command.ExecuteNonQuery();
                }
                connection.Close();
                return r;
            }
        }
        #endregion

        #region Comments

        public IEnumerable<Comment> GetComments (int TaskId)
        {
            var tl = new List<Comment>();

            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT [id], [Text], [Creator], [DateAdded] from [Comments] WHERE [Items_id] = ?";
                    contents.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = TaskId });
                    var r = contents.ExecuteReader();
                    while (r.Read())
                    {
                        Comment c = new Comment();
                        c.Id = Convert.ToInt32(r["id"]);
                        c.Text = r["Text"].ToString();
                        c.DateAdded = r["DateAdded"].ToString();
                        c.Creator = r["Creator"].ToString();

                        tl.Add(c);
                    }
                }
                connection.Close();
            }
            return tl;
        }

        public int AddNewComment (Comment item)
        {
            int r;
            lock (locker)
            {
                if (item.Id != 0)
                {
                    connection = new SqliteConnection("Data Source=" + path);
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE [Category] SET [Name] = ?, [Items_id] = ?, [Creator] = ?, [DateAdded] = ? WHERE [_id] = ?;";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Text });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.ToDoItemId });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Creator });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.DateAdded });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.Id });
                        r = command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return r;
                }
                else {
                    connection = new SqliteConnection("Data Source=" + path);
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [Comments] ([Text], [Items_id], [Creator], [DateAdded]) VALUES (?, ?, ?, ?)";
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Text });
                        command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.ToDoItemId });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Creator });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.DateAdded });
                        r = command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return r;
                }
            }
        }

        public int DeleteComment (int id)
        {
            lock (locker)
            {
                int r;
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Comments] WHERE [id] = ?;";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    r = command.ExecuteNonQuery();
                }
                connection.Close();
                return r;
            }
        }

        #endregion
    }
}