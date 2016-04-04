using ComeTogether.Models;
using ComeTogether.Services;
using ComeTogether.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Controllers
{
    public class MainController : Controller
    {
        private IMailService _mailService;
        private ITasksRepository _repos;

        public MainController(IMailService mailService, ITasksRepository repos)
        {
            _mailService = mailService;
            _repos = repos;
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
        public IActionResult Contact(ContactViewModel model)
        {
            var emailToReceive = Startup.Configuration["AppSettings:EmailAddressToReceive"];

            if (ModelState.IsValid)
            {

                if (_mailService.SendMessage(model.Email, "Me", model.Name, model.Message))
                {
                    ViewBag.Message = "Mail Sent.";
                    ModelState.Clear();
                }

            }

            return View();
        }

        public IActionResult Constructor()
        {
            return View();
        }

        [Authorize]
        public IActionResult Categories()
        {
            return View();
        }

        [Authorize]
        public IActionResult Users()
        {
            return View();
        }

        [Authorize]
        public IActionResult Recycler()
        {
            return View();
        }
    }
}