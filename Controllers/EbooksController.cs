using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EbookTest.Models;
using EbookTest.data;
using Microsoft.AspNetCore.Authorization;

namespace EbookTest.Controllers
{
    [Authorize (Roles = "Administrator")]
    public class EbooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EbooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ebooks
        public async Task<IActionResult> Index()
        {
              return View(await _context.ebooks.ToListAsync());
        }

        // GET: Ebooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ebooks == null)
            {
                return NotFound();
            }

            var ebook = await _context.ebooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ebook == null)
            {
                return NotFound();
            }

            return View(ebook);
        }

        // GET: Ebooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ebooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,auteur,Description,prix,DisplayOrder,CreatedDateTime,ImageUrl,stock")] Ebook ebook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ebook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ebook);
        }

        // GET: Ebooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ebooks == null)
            {
                return NotFound();
            }

            var ebook = await _context.ebooks.FindAsync(id);
            if (ebook == null)
            {
                return NotFound();
            }
            return View(ebook);
        }

        // POST: Ebooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,auteur,Description,prix,DisplayOrder,CreatedDateTime,ImageUrl,stock")] Ebook ebook)
        {
            if (id != ebook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ebook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EbookExists(ebook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ebook);
        }

        // GET: Ebooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ebooks == null)
            {
                return NotFound();
            }

            var ebook = await _context.ebooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ebook == null)
            {
                return NotFound();
            }

            return View(ebook);
        }

        // POST: Ebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ebooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ebooks'  is null.");
            }
            var ebook = await _context.ebooks.FindAsync(id);
            if (ebook != null)
            {
                _context.ebooks.Remove(ebook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EbookExists(int id)
        {
          return _context.ebooks.Any(e => e.Id == id);
        }
    }
}
