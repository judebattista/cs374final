using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Albums
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

        public DetailsModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        public Models.Albums Albums { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Albums = await _context.Albums.SingleOrDefaultAsync(m => m.Id == id);

            if (Albums == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
