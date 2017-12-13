using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Artists
{
    public class CreateModel : PageModel
    {
        private readonly Models.jazzRecordingDatabaseContext _context;

        public CreateModel(Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Artists Artists { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Artists.Add(Artists);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}