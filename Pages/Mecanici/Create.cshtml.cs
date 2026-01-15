using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Pages.Mecanici
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
            return Page();
        }

        [BindProperty]
        public Mecanic Mecanic { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mecanic.Add(Mecanic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
