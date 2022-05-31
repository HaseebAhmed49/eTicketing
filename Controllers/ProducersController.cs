using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicketing.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;

        public ProducersController(AppDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/

        // Index is default controller
        public async Task<IActionResult> Index()
        {
            var allProduces = await _context.Producers.ToListAsync();
            return View(allProduces);
            // IF you have a view with a different name, then you have to send the all producers data to that view use below
         //   return View("IndexView",allProduces);
        }
    }
}

