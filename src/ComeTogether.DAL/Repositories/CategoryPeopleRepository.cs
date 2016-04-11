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
            var user = _context.People.Where(c => c.Id == categoryPeople.UserId).FirstOrDefault();
            _context.CategoryPeople.Add(categoryPeople);

            //var category = _context.Category.Where(c => c.Id == categoryPeople.CategoryId).FirstOrDefault();
            //category.CategoryPeople.Add(categoryPeople);
        }

        public void DeleteCategoryPeople(CategoryPeople categoryPeople)
        {
            _context.CategoryPeople.Remove(categoryPeople);
        }
    }
}
