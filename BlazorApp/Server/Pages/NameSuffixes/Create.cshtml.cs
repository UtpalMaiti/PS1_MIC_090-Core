using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlazorApp.Server.Data;
using BlazorApp.Server.Models;

namespace BlazorApp.Server
{
    public class CreateModel : PageModel
    {
        private readonly BlazorApp.Server.Data.ApplicationDbContext _context;

        public CreateModel(BlazorApp.Server.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NameSuffix NameSuffix { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.NameSuffix == null || NameSuffix == null)
            {
                return Page();
            }

            _context.NameSuffix.Add(NameSuffix);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
