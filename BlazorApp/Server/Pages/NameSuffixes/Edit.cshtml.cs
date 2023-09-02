using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Server.Data;
using BlazorApp.Server.Models;

namespace BlazorApp.Server
{
    public class EditModel : PageModel
    {
        private readonly BlazorApp.Server.Data.ApplicationDbContext _context;

        public EditModel(BlazorApp.Server.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NameSuffix NameSuffix { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.NameSuffix == null)
            {
                return NotFound();
            }

            var namesuffix =  await _context.NameSuffix.FirstOrDefaultAsync(m => m.SuffixId == id);
            if (namesuffix == null)
            {
                return NotFound();
            }
            NameSuffix = namesuffix;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NameSuffix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NameSuffixExists(NameSuffix.SuffixId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NameSuffixExists(int id)
        {
          return (_context.NameSuffix?.Any(e => e.SuffixId == id)).GetValueOrDefault();
        }
    }
}
