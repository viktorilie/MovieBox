using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBox.Data;
using MovieBox.Models;
using MovieBox.Utility;

namespace MovieBox.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class DirectorController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DirectorController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Director.ToListAsync());

        }


        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Director director)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Director.Add(director);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(director);
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var director = await _db.Director.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                _db.Update(director);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }



        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var director = await _db.Director.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var director = await _db.Director.FindAsync(id);

            if (director == null)
            {
                return View();
            }
            _db.Director.Remove(director);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _db.Director.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }



    }
}