using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Tracks
{
    public class DeleteModel : PageModel
    {
        private readonly Models.jazzRecordingDatabaseContext _context;

        public DeleteModel(Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Tracks Tracks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tracks = await _context.Tracks.SingleOrDefaultAsync(m => m.Id == id);

            if (Tracks == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tracks = await _context.Tracks.FindAsync(id);

            if (Tracks != null)
            {
                _context.Tracks.Remove(Tracks);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
