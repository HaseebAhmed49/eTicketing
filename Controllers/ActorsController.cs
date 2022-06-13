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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTicketing.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorInterface _service;

        public ActorsController(IActorInterface service)
        {
            _service = service;
        }

        [AllowAnonymous]
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {            
            var data = await _service.GetAllASync();
                return View(data);
        }

        // Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }


        // Bind Properties that user will enter. Id will not be user by default so we dont bind that.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddASync(actor);
            return RedirectToAction(nameof(Index));
        }

        // Get: Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdASync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        // Get: Actors/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdASync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }


        // Bind Properties that user will enter. Id will not be user by default so we dont bind that.
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureUrl,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateASync(id,actor);
            return RedirectToAction(nameof(Index));
        }

        // Delete Actors/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdASync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }


        // Bind Properties that user will enter. Id will not be user by default so we dont bind that.
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdASync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteASync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

