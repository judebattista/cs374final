using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Albums
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

        public EditModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Albums).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!AlbumsExists(Albums.Id))
				if (!_context.Albums.Any(e => e.Id == Albums.Id))
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

        private bool AlbumsExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}
