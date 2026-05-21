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

namespace Management_Atelier_Reparatii.Pages.Masini
{
    using Microsoft.AspNetCore.Authorization;
    [Authorize(Roles = "Admin,Customer")]
    public class IndexModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;var mecanicEmail = "mecanic@local";
var mecanicPassword = "Mecanic123!";
var mecanic = userManager.FindByEmailAsync(mecanicEmail).GetAwaiter().GetResult();
if (mecanic == null)
{
    mecanic = new IdentityUser { UserName = mecanicEmail, Email = mecanicEmail, EmailConfirmed = true };
    var createResult = userManager.CreateAsync(mecanic, mecanicPassword).GetAwaiter().GetResult();
    if (createResult.Succeeded)
    {
        userManager.AddToRoleAsync(mecanic, "Mecanic").GetAwaiter().GetResult();
    }
}
else if (!userManager.IsInRoleAsync(mecanic, "Mecanic").GetAwaiter().GetResult())
{
    userManager.AddToRoleAsync(mecanic, "Mecanic").GetAwaiter().GetResult();
}
        }

        public IList<Masina> Masina { get;set; } = default!;

        public async Task OnGetAsync(string? searchString)
        {
            IQueryable<Masina> query = _context.Masina.Include(m => m.Client);

            if (User.IsInRole("Customer"))
            {
                var user = await _userManager.GetUserAsync(User);
                var client = await _context.Client.FirstOrDefaultAsync(c => c.Email == user.Email);
                if (client == null)
                {
                    Masina = new List<Masina>();
                    return;
                }
                query = query.Where(m => m.ClientId == client.Id);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(m => m.NumarInmatriculare.Contains(searchString));
            }

            Masina = await query.ToListAsync();
        }
    }
}

