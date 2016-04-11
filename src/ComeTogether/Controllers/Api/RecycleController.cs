using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net;
using ComeTogether.Models;
using ComeTogether.ViewModels;
using AutoMapper;
using ComeTogether.DAL.Interfaces;
using ComeTogether.DAL.Entities;

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    public class RecycleController : Controller
    {
        private IUnitOfWork _repository;

        public RecycleController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        [Route("api/recycle")]
        [HttpGet]
        public JsonResult GetDeletedTasks()
        {
            try
            {
                var tasks = _repository.ToDoItems.GetDeletedToDoItems();

                if (tasks == null)
                    return Json(null);

                return Json(Mapper.Map<IEnumerable<ToDoItemViewModel>>(tasks));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }
        }
        
        [HttpPut]
        [Route("api/recycle/{taskId}")]
        public JsonResult Put(int taskId, [FromBody]ToDoItemViewModel updatedToDoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editToDoItem = Mapper.Map<TodoItem>(updatedToDoVM);

                    _repository.ToDoItems.EditToDoItem(taskId, editToDoItem);

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

        [Route("api/recycle/{taskId}")]
        [HttpDelete]
        public JsonResult Delete(int taskId)
        {
            try
            {
                _repository.ToDoItems.DeleteToDoItem(taskId);

                if (_repository.SaveChanges())
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(new { Message = "ToDo Item -" + taskId + "has been Deleted" });
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
                
        [Route("api/recycle/deleteAllDeleted")]
        [HttpDelete]
        public JsonResult DeleteAllDeleted()
        {
            try
            {
                _repository.ToDoItems.DeleteAllDeletedItems();

                if (_repository.SaveChanges())
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                }
                return Json(new { Message = "Tasks was been deleted." });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.ToString() });
            }
        }
    }
}
