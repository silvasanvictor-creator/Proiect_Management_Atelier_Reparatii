using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Pages.ComenziService
{
    public class CreateModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;

        public CreateModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MasinaId"] = new SelectList(_context.Set<Masina>(), "MasinaId", "MasinaId");
        ViewData["MecanicId"] = new SelectList(_context.Set<Mecanic>(), "MecanicId", "MecanicId");
            return Page();
        }

        [BindProperty]
        public ComandaService ComandaService { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ComandaService.Add(ComandaService);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
