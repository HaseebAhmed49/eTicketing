using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicketing.Data;
using eTicketing.Data.Services;
using eTicketing.Data.Static;
using eTicketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }
        // GET: /<controller>/

        // Index is default controller
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProduces = await _service.GetAllASync();
            return View(allProduces);
            // IF you have a view with a different name, then you have to send the all producers data to that view use below
         //   return View("IndexView",allProduces);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdASync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")] Producer producer)
        {
            if(!ModelState.IsValid) return View(producer);
            await _service.AddASync(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producers = await _service.GetByIdASync(id);
            if (producers == null) return View("NotFound");
            return View(producers);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureUrl,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            if(id == producer.Id)
            {
                await _service.UpdateASync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producers = await _service.GetByIdASync(id);
            if (producers == null) return View("NotFound");
            return View(producers);
        }


        [HttpDelete,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producers = await _service.GetByIdASync(id);
            if (producers == null) return View("NotFound");

            await _service.DeleteASync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

