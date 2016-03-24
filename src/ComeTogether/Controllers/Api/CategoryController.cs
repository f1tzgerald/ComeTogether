using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Models;
using System.Net;
using ComeTogether.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Authorization;

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private ITasksRepository _repository;

        public CategoryController(ITasksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var res = Mapper.Map<IEnumerable<CategoryViewModel>>(_repository.GetAllCategories());

            return Json(res);
        }

        [HttpPost]
        public JsonResult Post([FromBody] CategoryViewModel viewmodelCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Add new category 
                    var category = Mapper.Map<Category>(viewmodelCategory);

                    _repository.AddCategory(category);

                    if (_repository.SaveChanges())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<CategoryViewModel>(category));
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

        [HttpPut]
        public JsonResult Put(string name, [FromBody] CategoryViewModel categoryVM)
        {
            if (name == categoryVM.Name)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { message = "Name hasn't changed" });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var editCategory = Mapper.Map<Category>(categoryVM);

                    _repository.EditCategory(name, editCategory);

                    if (_repository.SaveChanges())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<CategoryViewModel>(editCategory));
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
    }
}
