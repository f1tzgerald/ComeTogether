using ComeTogether.Services;
using ComeTogether.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Controllers
{
    public class MainController : Controller
    {
        IMailService _mailService;

        public MainController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact([FromBody] ContactViewModel model)
        {

            return View();
        }

        public IActionResult Constructor()
        {
            return View();
        }
    }
}
