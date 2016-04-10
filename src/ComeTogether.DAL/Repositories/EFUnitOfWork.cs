using ComeTogether.DAL.EntityFramework;
using ComeTogether.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ComeTogetherContext _context;

        private CategoryRepository categoryRepository;
        private CommentRepository commentRepository;
        private ToDoItemRepository todoitemRepository;
        private UserRepository userRepository;
        private CategoryPeopleRepository categoryPeopleRepository;

        public EFUnitOfWork(/*string connectionString*/)
        {
            _context = new ComeTogetherContext(/*connectionString*/);
        }

        public ICategory Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(_context);
                return categoryRepository;
            }
        }

        public IComment Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new CommentRepository(_context);
                return commentRepository;
            }
        }

        public IUser People
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_context);
                return userRepository;
            }
        }

        public IToDoItem ToDoItems
        {
            get
            {
                if (todoitemRepository == null)
                    todoitemRepository = new ToDoItemRepository(_context);
                return todoitemRepository;
            }
        }

        public ICategoryPeople CategoryPeople
        {
            get
            {
                if (categoryPeopleRepository == null)
                    categoryPeopleRepository = new CategoryPeopleRepository(_context);
                return categoryPeopleRepository;
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
