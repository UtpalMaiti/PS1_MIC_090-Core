using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Server.Data;
using BlazorApp.Server.Models;

namespace BlazorApp.Server
{
    public class DetailsModel : PageModel
    {
        private readonly BlazorApp.Server.Data.ApplicationDbContext _context;

        public DetailsModel(BlazorApp.Server.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public NameSuffix NameSuffix { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.NameSuffix == null)
            {
                return NotFound();
            }

            var namesuffix = await _context.NameSuffix.FirstOrDefaultAsync(m => m.SuffixId == id);
            if (namesuffix == null)
            {
                return NotFound();
            }
            else 
            {
                NameSuffix = namesuffix;
            }
            return Page();
        }
    }
}
