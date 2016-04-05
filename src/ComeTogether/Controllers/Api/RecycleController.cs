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

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    public class RecycleController : Controller
    {
        private ITasksRepository _repository;

        public RecycleController(ITasksRepository repository)
        {
            _repository = repository;
        }

        [Route("api/recycle")]
        [HttpGet]
        public JsonResult GetDeletedTasks()
        {
            try
            {
                var tasks = _repository.GetDeletedToDoItems();

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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        [Route("api/recycle/{taskId}")]
        [HttpDelete]
        public JsonResult Delete(int taskId)
        {
            try
            {
                _repository.DeleteToDoItem(taskId);

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
                
        [Route("api/recycle/deleteAllDone")]
        [HttpDelete]
        public JsonResult DeleteAllDone(int categoryId)
        {
            try
            {
                _repository.DeleteAllDoneItems(categoryId);

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
