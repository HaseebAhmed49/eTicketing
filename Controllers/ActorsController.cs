using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicketing.Data;
using eTicketing.Data.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorInterface _service;

        public ActorsController(IActorInterface service)
        {
            _service = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var data = _service.GetAll();
                return View(data);
        }
    }
}

