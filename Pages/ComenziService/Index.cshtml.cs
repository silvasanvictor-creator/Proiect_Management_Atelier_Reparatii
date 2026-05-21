using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Management_Atelier_Reparatii.Pages.ComenziService
{
    public class IndexModel : PageModel
    {
        private readonly Management_Atelier_ReparatiiContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(Management_Atelier_ReparatiiContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ComandaService> ComandaService { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // If the logged-in user has a Client record (matched by email) show only their comenzi
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var client = await _context.Client.FirstOrDefaultAsync(c => c.Email == user.Email);
                if (client != null)
                {
                    ComandaService = await _context.ComandaService
                        .Include(c => c.Masina)
                        .Include(c => c.Mecanic)
                        .Where(c => c.Masina != null && c.Masina.ClientId == client.Id)
                        .ToListAsync();
                    return;
                }
            }

            // Admin & Mecanic see all
            if (User.IsInRole("Admin") || User.IsInRole("Mecanic"))
            {
                ComandaService = await _context.ComandaService
                    .Include(c => c.Masina)
                    .Include(c => c.Mecanic)
                    .ToListAsync();
                return;
            }

            // Other users (not linked to a Client) see nothing
            ComandaService = new List<ComandaService>();
        }

        // Server-side handler to change the status from the Index page
        public async Task<IActionResult> OnPostChangeStatusAsync(int id, StatusComanda newStatus)
        {
            // Only Admins and Mechanics may change status
            if (!(User.IsInRole("Admin") || User.IsInRole("Mecanic")))
            {
                return Forbid();
            }

            var comanda = await _context.ComandaService.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }

            comanda.Status = newStatus;
            await _context.SaveChangesAsync();

            // Reload the Index page to show updated status
            return RedirectToPage();
        }
    }
}
