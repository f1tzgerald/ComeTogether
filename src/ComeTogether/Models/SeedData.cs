using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Models
{
    public class SeedData
    {
        private MainContextDb _context;
        private UserManager<Person> _userManager;

        public SeedData(MainContextDb context, UserManager<Person> manager)
        {
            _context = context;
            _userManager = manager;
        }

        public async Task AddDataAsync()
        {
            Person first = new Person() { Created = DateTime.Now.Date, UserName = "Vitalii", Email = "alter.vetal@mail.ru" };
            Person second = new Person() { Created = DateTime.Now.Date, UserName = "Carl", Email = "vvv@mail.ru" };
            Person first1 = new Person() { Created = DateTime.Now.Date, UserName = "Misja", Email = "c@mail.ru" };
            Person second2 = new Person() { Created = DateTime.Now.Date, UserName = "Oleg", Email = "v@mail.ru" };

            if (await _userManager.FindByEmailAsync("alter.vetal@mail.ru") == null)
            {
                await _userManager.CreateAsync(first, "P@ssw0rd!");
                await _userManager.CreateAsync(second, "P@ssw0rd!");
                await _userManager.CreateAsync(first1, "P@ssw0rd!");
                await _userManager.CreateAsync(second2, "P@ssw0rd!");
            }

            if (!_context.Category.Any())
            {
                #region Add Category I
                var todo1 = new TodoItem()
                {
                    Name = "1",
                    Creator = "Vitalii",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    IsDeleted = false,
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
                    Creator = "Vitalii",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    IsDeleted = false,
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

                    ToDoItems = new List<TodoItem>() { todo1, todo2 },
                };

                _context.Add(newCategory);
                _context.AddRange(newCategory.ToDoItems);
                _context.AddRange(todo1.Comments);
                _context.AddRange(todo2.Comments);
                _context.SaveChanges();
                #endregion

                #region Add Category II
                var todo3 = new TodoItem()
                {
                    Name = "3",
                    Creator = "Me",
                    DateAdded = DateTime.UtcNow.Date,
                    DateFinish = DateTime.UtcNow.AddDays(1).Date,
                    Done = false,
                    IsDeleted = false,
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
                    IsDeleted = false,
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
                _context.SaveChanges();
                #endregion

                var useraltId = _context.People.Where(c => c.Email == "alter.vetal@mail.ru").FirstOrDefault().Id;
                var user2Id = _context.People.Where(c => c.Email == "vvv@mail.ru").FirstOrDefault().Id;

                CategoryPeople cp = new CategoryPeople() { CategoryId = 1, UserId = useraltId };
                CategoryPeople cp1 = new CategoryPeople() { CategoryId = 2, UserId = useraltId };
                CategoryPeople cp2 = new CategoryPeople() { CategoryId = 2, UserId = user2Id };
                _context.AddRange(cp, cp1, cp2);

                _context.SaveChanges();
            }
        }
    }
}
