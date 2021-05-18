using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheGStore.Models;
using TheGStore;

namespace TheGStore.Controllers
{
    public class GenresController : Controller
    {
        private readonly TheGStoreDbContext _context;

        public GenresController(TheGStoreDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Genre = _context.Genres.Find(id).Name;
            return View();
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            bool duplicate = await _context.Genres.AnyAsync(d => d.Name.Equals(genre.Name));

            if (duplicate)
            {
                ModelState.AddModelError("FirstName", Resourses.ERROR_GenreExists);
            }

            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var genres = await _context.Genres.FindAsync(id);
            return View(genres);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {
            bool duplicate = await _context.Genres.AnyAsync(d => d.Name.Equals(genre.Name));

            if (duplicate)
            {
                ModelState.AddModelError("FirstName", Resourses.ERROR_GenreExists);
            }

            if (ModelState.IsValid)
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genres = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genres);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}