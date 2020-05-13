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
    public class GenreController : Controller
    {

        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }


        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Genre.ToListAsync());

        }


        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }



        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Genre.Add(genre);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(genre);
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = await _db.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.Update(genre);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }



        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = await _db.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var genre = await _db.Genre.FindAsync(id);

            if (genre == null)
            {
                return View();
            }
            _db.Genre.Remove(genre);
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

            var genre = await _db.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }






    }
}