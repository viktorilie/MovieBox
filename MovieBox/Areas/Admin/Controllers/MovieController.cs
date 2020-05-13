using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBox.Data;
using MovieBox.Models.ViewModels;
using MovieBox.Utility;
using MovieBox.Models;
using Microsoft.AspNetCore.Authorization;

namespace MovieBox.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class MovieController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public MovieViewModel MovieVM { get; set; }

        public MovieController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            MovieVM = new MovieViewModel()
            {
                Genre = _db.Genre,
                Movie = new Models.Movie()
            };
        }

        public async Task<IActionResult> Index()
        {
            var movie = await _db.Movie.Include(m => m.Genre).ToListAsync();
            return View(movie);
        }




        //GET - CREATE
        public IActionResult Create()
        {
            return View(MovieVM);
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {

            if (!ModelState.IsValid)
            {
                return View(MovieVM);
            }

            _db.Movie.Add(MovieVM.Movie);
            await _db.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var movieFromDb = await _db.Movie.FindAsync(MovieVM.Movie.Id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, MovieVM.Movie.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                movieFromDb.Image = @"\images\" + MovieVM.Movie.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultMovieImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + MovieVM.Movie.Id + ".png");
                movieFromDb.Image = @"\images\" + MovieVM.Movie.Id + ".png";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieVM.Movie = await _db.Movie.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (MovieVM.Movie == null)
            {
                return NotFound();
            }
            return View(MovieVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(MovieVM);
            }

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.Movie.FindAsync(MovieVM.Movie.Id);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, menuItemFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //we will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, MovieVM.Movie.Id + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.Image = @"\images\" + MovieVM.Movie.Id + extension_new;
            }

            menuItemFromDb.Name = MovieVM.Movie.Name;
            menuItemFromDb.Description = MovieVM.Movie.Description;
            menuItemFromDb.Price = MovieVM.Movie.Price;
            menuItemFromDb.GenreId = MovieVM.Movie.GenreId;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET : DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieVM.Movie = await _db.Movie.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (MovieVM.Movie == null)
            {
                return NotFound();
            }

            return View(MovieVM);
        }


        //GET : DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieVM.Movie = await _db.Movie.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (MovieVM.Movie == null)
            {
                return NotFound();
            }

            return View(MovieVM);
        }

        //POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Movie movie = await _db.Movie.FindAsync(id);

            if (movie != null)
            {
                var imagePath = Path.Combine(webRootPath, movie.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.Movie.Remove(movie);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }



    }
}