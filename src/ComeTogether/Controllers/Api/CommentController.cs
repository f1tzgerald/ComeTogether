using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ComeTogether.Controllers.Api
{
    [Authorize]
    [Route("api/category/{categoryId}/{taskId}/comments")]
    public class CommentController : Controller
    {

    }
}
