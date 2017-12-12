using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Artists
{
    public class DetailsModel : PageModel
    {
        private readonly jazzDatabaseContext _context;

        public DetailsModel(jazzDatabaseContext context)
        {
            _context = context;
        }

        public Models.Artists Artists { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Artists = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);

            if (Artists == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
