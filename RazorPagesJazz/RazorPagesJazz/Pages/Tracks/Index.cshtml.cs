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
    public class IndexModel : PageModel
    {
        private readonly jazzDatabaseContext _context;

        public IndexModel(jazzDatabaseContext context)
        {
            _context = context;
        }

        public IList<Tracks> Tracks { get;set; }

        public async Task OnGetAsync()
        {
            Tracks = await _context.Tracks.ToListAsync();
        }
    }
}
