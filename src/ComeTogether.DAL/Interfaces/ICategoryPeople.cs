using ComeTogether.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Interfaces
{
    public interface ICategoryPeople
    {
        void AddCategoryPeople(CategoryPeople item);
        void DeleteCategoryPeople(CategoryPeople item);
    }
}
