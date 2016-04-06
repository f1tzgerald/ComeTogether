using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ComeTogether.Models;
using System.Net;
using AutoMapper;
using ComeTogether.ViewModels;
using Microsoft.AspNet.Authorization;

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    public class ToDoItemController : Controller
    {
        private ITasksRepository _repository;

        public ToDoItemController(ITasksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/category/{categoryId}/tasks")]
        public JsonResult Get(int categoryId)
        {
            try
            {
                var tasks = _repository.GetToDoItemsForCategory(categoryId);

                if (tasks == null)
                    return Json(null);

                return Json(Mapper.Map<IEnumerable<ToDoItemViewModel>>(tasks));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }
        }

        [HttpPost]
        [Route("api/category/{categoryId}/tasks")]
        public JsonResult Post(int categoryId, [FromBody] ToDoItemViewModel toDoitemVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoItem = Mapper.Map<TodoItem>(toDoitemVM);
                    todoItem.Creator = User.Identity.Name;

                    _repository.AddToDoItem(categoryId, todoItem);

                    if (_repository.SaveChanges())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<ToDoItemViewModel>(todoItem));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.ToString() });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(false);
        }

        //PUT api/values/5
        [HttpPut]
        [Route("api/category/{categoryId}/tasks/{todoitemId}")]
        public JsonResult Put(int todoitemId, [FromBody]ToDoItemViewModel updatedToDoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editToDoItem = Mapper.Map<TodoItem>(updatedToDoVM);

                    _repository.EditToDoItem(todoitemId, editToDoItem);

                    if (_repository.SaveChanges())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<ToDoItemViewModel>(editToDoItem));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.ToString() });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(false);
        }

        // DELETE all done tasks
        [HttpPut]
        [Route("api/category/{categoryId}/tasks/deleteAllDone")]
        public JsonResult DeleteAllDone(int categoryId)
        {
            try
            {
                _repository.MoveAllDoneItemsToRecycle(categoryId);

                if (_repository.SaveChanges())
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                }
                return Json(new { Message = "Tasks was been deleted to recycle." });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.ToString() });
            }
        }
    }
}
