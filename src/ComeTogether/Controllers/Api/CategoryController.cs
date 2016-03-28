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
    public class CategoryController : Controller
    {
        private ITasksRepository _repository;

        public CategoryController(ITasksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/category")]
        public JsonResult Get()
        {
            var res = Mapper.Map<IEnumerable<CategoryViewModel>>(_repository.GetAllCategories());

            return Json(res);
        }

        [HttpGet]
        [Route("api/category/{categoryId}")]
        public JsonResult Get(int categoryId)
        {
            var category = _repository.GetCategoryById(categoryId);

            return Json(Mapper.Map<CategoryViewModel>(category));
        }

        [HttpPost]
        [Route("api/category")]
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

        
        [Route ("api/category/edit/{categoryId}")]
        [HttpPut]
        public JsonResult Put(int categoryId, [FromBody] CategoryViewModel editCategoryVM)
            {
            try
            {
                if (ModelState.IsValid)
                {
                    var editCategory = Mapper.Map<Category>(editCategoryVM);

                    _repository.EditCategory(categoryId, editCategory);

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

        [HttpDelete]
        [Route("api/category/{categoryId}")]
        public JsonResult Delete (int categoryId)
        {
            try
            {
                if (_repository.GetCategoryById(categoryId) != null)
                {
                    var categoryToDelete = _repository.GetCategoryById(categoryId);

                    _repository.DeleteCategory(categoryId);

                    if (_repository.SaveChanges())
                    {
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json(new { Message = $"Category {categoryToDelete.Name} has been deleted." });
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.ToString() });
            }
            
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = $"Can't find category with this id:{categoryId}." });
        }
    }
}
