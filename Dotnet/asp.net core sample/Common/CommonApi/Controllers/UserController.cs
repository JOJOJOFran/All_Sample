using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommonApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IRepositories _repositories;

        public UserController(IRepositories repositories)
        {
            _repositories = repositories.GetRepositories("User");
        }


        [HttpGet]
        public async Task<ActionResult> Test()
        {
            _repositories.Add();
            return Json(new string[] { "value1", "value2" });
        }
    }
}
