﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using ComeTogether.Models;
using AutoMapper;
using ComeTogether.ViewModels;
using System.Net;

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    [Route("api/category/{categoryId}/{taskId}/comments")]
    public class CommentController : Controller
    {
        private ITasksRepository _repository;

        public CommentController(ITasksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult Get(int taskId)
        {
            try
            {
                var toDoItem = _repository.GetToDoItemById(taskId);

                if (toDoItem == null)
                    return Json(null);

                return Json(Mapper.Map<IEnumerable<CommentViewModel>>(toDoItem.Comments));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }
        }

        [HttpPost]
        public JsonResult Post(int taskId, [FromBody] CommentViewModel commentnewVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var commentToAdd = Mapper.Map<Comment>(commentnewVM);
                    commentToAdd.Creator = User.Identity.Name;

                    _repository.AddComment (taskId, commentToAdd);

                    if (_repository.SaveChanges())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<CommentViewModel>(commentToAdd));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Error: {ex.ToString()}");
            }

            // If model is not valid
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(false);
        }
    }
}