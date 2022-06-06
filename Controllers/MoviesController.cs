using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicketing.Data;
using eTicketing.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        // GET: /<controller>/

        // Index is default controller
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllASync(n=> n.Cinema);
            return View(data);
        }

        // Get: Movies/Details
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdASync(id);
            return View(movieDetails);

        }

        // Get: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }
    }
}

