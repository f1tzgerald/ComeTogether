using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Models
{
    public class SeedData
    {
        private MainContextDb _context;

        public SeedData(MainContextDb context)
        {
            _context = context;

        }

        public void AddData()
        {
            if (!_context.Category.Any())
            {
                #region Add Category I
                var todo1 = new TodoItem()
                {
                    Name = "1",
                    Creator = "Me",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    WhoDoIt = "Marry",
                    Comments = new List<Comment>()
                            {
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="first comment",
                                    Creator ="Me"
                                },
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="Second comment",
                                    Creator ="SomeOne else"
                                }
                            }
                };
                var todo2 = new TodoItem()
                {
                    Name = "2",
                    Creator = "Me",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    WhoDoIt = "Marry",
                    Comments = new List<Comment>()
                            {
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="third comment",
                                    Creator ="Me"
                                },
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="fourth comment",
                                    Creator ="SomeOne else"
                                }
                            }
                };

                var newCategory = new Category()
                {
                    Name = "First Category",
                    ToDoItems = new List<TodoItem>() { todo1, todo2 }
                };

                _context.Add(newCategory);
                _context.AddRange(newCategory.ToDoItems);
                _context.AddRange(todo1.Comments);
                _context.AddRange(todo2.Comments);
#endregion

                #region Add Category II
                var todo3 = new TodoItem()
                {
                    Name = "3",
                    Creator = "Me",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    WhoDoIt = "Marry",
                    Comments = new List<Comment>()
                            {
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="first comment",
                                    Creator ="Me"
                                },
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="Second comment",
                                    Creator ="SomeOne else"
                                }
                            }
                };

                var todo4 = new TodoItem()
                {
                    Name = "4",
                    Creator = "Me",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    WhoDoIt = "Marry",
                    Comments = new List<Comment>()
                            {
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="third comment",
                                    Creator ="Me"
                                },
                                new Comment ()
                                {
                                    DateAdded = DateTime.UtcNow,
                                    Text ="fourth comment",
                                    Creator ="SomeOne else"
                                }
                            }
                };

                var secondCategory = new Category()
                {
                    Name = "Second Category",
                    ToDoItems = new List<TodoItem>() { todo3, todo4 }
                };

                _context.Add(secondCategory);
                _context.AddRange(secondCategory.ToDoItems);
                _context.AddRange(todo3.Comments);
                _context.AddRange(todo4.Comments);
                #endregion

                _context.SaveChanges();
            }
        }
    }
}
