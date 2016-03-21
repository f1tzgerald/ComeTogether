using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ComeTogether.Models;

namespace ComeTogether.Controllers
{
    public class CategoryController : Controller
    {
        private MainContextDb _context;

        public CategoryController(MainContextDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Category.OrderBy(n=>n.Name).ToList();
            return View(categories);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
