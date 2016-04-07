using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using ComeTogether.Models;
using System.Net;
using ComeTogether.DAL.Interfaces;

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    public class UserController : Controller
    {
        private IUnitOfWork _repository;

        public UserController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/currentuser")]
        public JsonResult GetCurrentUser()
        {
            return Json(new { currentUser = User.Identity.Name });
        }

        [HttpGet]
        [Route("api/users/{countTake}/{countSkip}")]
        public JsonResult GetAllUsers(int countTake, int countSkip)
        {
            try
            {
                var users = _repository.People.GetCountOfUsersFrom(countTake, countSkip);

                if (users != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(users);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }            
        }

        [HttpGet]
        [Route("api/category/{categoryId}/users")]
        public JsonResult GetUsersForCategory(int categoryId)
        {
            try
            {
                var people = _repository.People.GetAllUsersForCategory(categoryId);

                if (people == null)
                    return Json(null);

                return Json(people);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }
        }

        [HttpPost]
        [Route("api/category/{categoryId}/users")]
        public JsonResult Post(int categoryId/*, Person personToAdd*/)
        {
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var todoItem = Mapper.Map<TodoItem>(toDoitemVM);
            //        todoItem.Creator = User.Identity.Name;

            //        _repository.AddToDoItem(categoryId, todoItem);

            //        if (_repository.SaveChanges())
            //        {
            //            Response.StatusCode = (int)HttpStatusCode.Created;
            //            return Json(Mapper.Map<ToDoItemViewModel>(todoItem));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return Json(new { Message = ex.ToString() });
            //}

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(false);
        }
    }
}
