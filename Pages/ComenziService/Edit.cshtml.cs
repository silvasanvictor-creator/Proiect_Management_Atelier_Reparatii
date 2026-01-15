using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Pages.ComenziService
{
    public class EditModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;

        public EditModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComandaService ComandaService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comandaservice =  await _context.ComandaService.FirstOrDefaultAsync(m => m.ComandaServiceId == id);
            if (comandaservice == null)
            {
                return NotFound();
            }
            ComandaService = comandaservice;
           ViewData["MasinaId"] = new SelectList(_context.Set<Masina>(), "MasinaId", "MasinaId");
           ViewData["MecanicId"] = new SelectList(_context.Set<Mecanic>(), "MecanicId", "MecanicId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ComandaService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaServiceExists(ComandaService.ComandaServiceId))
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

        private bool ComandaServiceExists(int id)
        {
            return _context.ComandaService.Any(e => e.ComandaServiceId == id);
        }
    }
}
