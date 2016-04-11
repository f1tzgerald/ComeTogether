using ComeTogether.DAL.Entities;
using ComeTogether.DAL.EntityFramework;
using ComeTogether.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Repositories
{
    public class UserRepository : IUser
    {
        private ComeTogetherContext _context;

        public UserRepository(ComeTogetherContext context)
        {
            _context = context;
        }
        public IEnumerable<Person> GetAllUsersForCategory(int categoryId)
        {
            return _context.CategoryPeople.Where(c => c.CategoryId == categoryId).Select(c => c.Person).ToList();
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _context.People.OrderBy(c => c.UserName).ToList();
        }

        public Person GetUserByName(string userName)
        {
            return _context.People.Where(c => c.UserName == userName).FirstOrDefault();
        }

        public IEnumerable<Person> GetCountOfUsersFrom(int count, int skip)
        {
            return _context.People.OrderBy(c => c.UserName).Skip(skip).Take(count).ToList();
        }
    }
}
