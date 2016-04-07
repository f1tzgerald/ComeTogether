using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategory Categories { get; }
        IComment Comments { get; }
        IToDoItem ToDoItems { get; }
        IUser People { get; }

        bool SaveChanges();
    }
}
