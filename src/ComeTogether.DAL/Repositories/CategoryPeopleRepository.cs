using ComeTogether.DAL.Entities;
using ComeTogether.DAL.EntityFramework;
using ComeTogether.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Repositories
{
    public class CategoryPeopleRepository : ICategoryPeople
    {
        private ComeTogetherContext _context;

        public CategoryPeopleRepository(ComeTogetherContext context)
        {
            _context = context;
        }

        public void AddCategoryPeople(CategoryPeople categoryPeople)
        {
            _context.CategoryPeople.Add(categoryPeople);
        }

        public void DeleteCategoryPeople(CategoryPeople categoryPeople)
        {
            _context.CategoryPeople.Remove(categoryPeople);
        }
    }
}
