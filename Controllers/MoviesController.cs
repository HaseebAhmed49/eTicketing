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
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/

        // Index is default controller
        public async Task<IActionResult> Index()
        {
            var data = await _context.Movies.ToListAsync();
            return View();
        }
    }
}

