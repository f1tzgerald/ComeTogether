using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ComeTogether.Models;
using ComeTogether.ViewModels;
using AutoMapper;
using System.Net;
using Microsoft.AspNet.Authorization;

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    [Route("api/category/edit/{categoryId}")]
    public class CategoryEditController : Controller
    {
        private ITasksRepository _repository;

        public CategoryEditController(ITasksRepository repos)
        {
            _repository = repos;
        }

        [HttpPut]
        public JsonResult Put(int categoryId, [FromBody]CategoryViewModel editCategoryVM)
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
    }
}
