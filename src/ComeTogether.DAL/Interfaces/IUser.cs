using ComeTogether.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Interfaces
{
    public interface IUser
    {
        IEnumerable<Person> GetAllUsersForCategory(int categoryId);
        IEnumerable<Person> GetAllUsers();
        IEnumerable<Person> GetCountOfUsersFrom(int count, int skip);
        Person GetUserByName(string userName);
    }
}
