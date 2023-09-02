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
    public class IndexModel : PageModel
    {
        private readonly BlazorApp.Server.Data.ApplicationDbContext _context;

        public IndexModel(BlazorApp.Server.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<NameSuffix> NameSuffix { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.NameSuffix != null)
            {
                NameSuffix = await _context.NameSuffix.ToListAsync();
            }
        }
    }
}
