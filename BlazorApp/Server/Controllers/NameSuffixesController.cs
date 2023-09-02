using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Server.Data;
using BlazorApp.Server.Models;

namespace BlazorApp.Server.Controllers
{
    public class NameSuffixesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NameSuffixesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NameSuffixes
        public async Task<IActionResult> Index()
        {
            return _context.NameSuffix != null ?
                        View(await _context.NameSuffix.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.NameSuffix'  is null.");
        }

        // GET: NameSuffixes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NameSuffix == null)
            {
                return NotFound();
            }

            var nameSuffix = await _context.NameSuffix
                .FirstOrDefaultAsync(m => m.SuffixId == id);
            if (nameSuffix == null)
            {
                return NotFound();
            }

            return View(nameSuffix);
        }

        // GET: NameSuffixes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NameSuffixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SuffixId,Suffix")] NameSuffix nameSuffix)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nameSuffix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nameSuffix);
        }

        // GET: NameSuffixes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NameSuffix == null)
            {
                return NotFound();
            }

            var nameSuffix = await _context.NameSuffix.FindAsync(id);
            if (nameSuffix == null)
            {
                return NotFound();
            }
            return View(nameSuffix);
        }

        // POST: NameSuffixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuffixId,Suffix")] NameSuffix nameSuffix)
        {
            if (id != nameSuffix.SuffixId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nameSuffix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NameSuffixExists(nameSuffix.SuffixId))
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
            return View(nameSuffix);
        }

        // GET: NameSuffixes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NameSuffix == null)
            {
                return NotFound();
            }

            var nameSuffix = await _context.NameSuffix
                .FirstOrDefaultAsync(m => m.SuffixId == id);
            if (nameSuffix == null)
            {
                return NotFound();
            }

            return View(nameSuffix);
        }

        // POST: NameSuffixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NameSuffix == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NameSuffix'  is null.");
            }
            var nameSuffix = await _context.NameSuffix.FindAsync(id);
            if (nameSuffix != null)
            {
                _context.NameSuffix.Remove(nameSuffix);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NameSuffixExists(int id)
        {
            return (_context.NameSuffix?.Any(e => e.SuffixId == id)).GetValueOrDefault();
        }
    }
}
