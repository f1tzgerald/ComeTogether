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
    [Route("api/category/{categoryName}/tasks")]
    public class ToDoItemController : Controller
    {
        private ITasksRepository _repository;

        public ToDoItemController(ITasksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult Get(string categoryName)
        {
            try
            {
                var category = _repository.GetCategoryByName(categoryName);

                if (category == null)
                    return Json(null);

                return Json(Mapper.Map<IEnumerable<ToDoItemViewModel>>(category.ToDoItems));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }
        }

        [HttpPost]
        public JsonResult Post(string categoryName, [FromBody] ToDoItemViewModel toDoitemVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoItem = Mapper.Map<TodoItem>(toDoitemVM);

                    _repository.AddToDoItem(categoryName, todoItem);

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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
