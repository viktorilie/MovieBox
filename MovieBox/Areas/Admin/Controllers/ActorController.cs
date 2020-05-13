﻿using System;
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
    public class ActorController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ActorController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Actor.ToListAsync());

        }


        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Actor.Add(actor);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(actor);
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var actor = await _db.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                _db.Update(actor);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }



        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var actor = await _db.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var actor = await _db.Actor.FindAsync(id);

            if (actor == null)
            {
                return View();
            }
            _db.Actor.Remove(actor);
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

            var actor = await _db.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }




    }



}