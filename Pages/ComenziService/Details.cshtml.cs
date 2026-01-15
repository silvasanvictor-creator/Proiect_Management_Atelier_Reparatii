using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Pages.ComenziService
{
    public class DetailsModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;

        public DetailsModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context)
        {
            _context = context;
        }

        public ComandaService ComandaService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comandaservice = await _context.ComandaService.FirstOrDefaultAsync(m => m.ComandaServiceId == id);

            if (comandaservice is not null)
            {
                ComandaService = comandaservice;

                return Page();
            }

            return NotFound();
        }
    }
}
