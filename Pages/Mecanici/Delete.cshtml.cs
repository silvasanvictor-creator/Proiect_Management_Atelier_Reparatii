using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Pages.Mecanici
{
    public class DeleteModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;

        public DeleteModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mecanic Mecanic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mecanic = await _context.Mecanic.FirstOrDefaultAsync(m => m.MecanicId == id);

            if (mecanic is not null)
            {
                Mecanic = mecanic;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mecanic = await _context.Mecanic.FindAsync(id);
            if (mecanic != null)
            {
                Mecanic = mecanic;
                _context.Mecanic.Remove(Mecanic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
