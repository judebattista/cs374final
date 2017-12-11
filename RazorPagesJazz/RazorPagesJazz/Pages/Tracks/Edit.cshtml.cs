using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Tracks
{
    public class EditModel : PageModel
    {
        private readonly jazzDatabaseContext _context;

        public EditModel(jazzDatabaseContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Tracks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TracksExists(Tracks.Id))
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

        private bool TracksExists(int id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }
    }
}
