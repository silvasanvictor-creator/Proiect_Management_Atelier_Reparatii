using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Management_Atelier_Reparatii.Data;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Pages.Clienti
{
    public class IndexModel : PageModel
    {
        private readonly Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext _context;

        public IndexModel(Management_Atelier_Reparatii.Data.Management_Atelier_ReparatiiContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Client = await _context.Client.ToListAsync();
        }
    }
}
