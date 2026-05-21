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
    [Authorize(Roles = "Admin,Mecanic")]
    public class DetailsModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ComandaService ComandaService { get; set; } = default!;

        // Add this property to hold the list of Masina objects
        public IList<Masina> Masina { get; set; } = new List<Masina>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comandaservice = await _context.ComandaService
                .Include(c => c.Masina)
                .Include(c => c.Mecanic)
                .FirstOrDefaultAsync(m => m.ComandaServiceId == id);

            if (comandaservice is not null)
            {
                ComandaService = comandaservice;

                return Page();
            }

            return NotFound();
        }

        // NOTE: search functionality removed from Details page to avoid routing ambiguity with OnGetAsync(int?)
    }
}
