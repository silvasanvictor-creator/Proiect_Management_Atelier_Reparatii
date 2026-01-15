using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Management_Atelier_Reparatii.Models;

namespace Management_Atelier_Reparatii.Data
{
    public class Management_Atelier_ReparatiiContext : DbContext
    {
        public Management_Atelier_ReparatiiContext (DbContextOptions<Management_Atelier_ReparatiiContext> options)
            : base(options)
        {
        }

        public DbSet<Management_Atelier_Reparatii.Models.Client> Client { get; set; } = default!;
        public DbSet<Management_Atelier_Reparatii.Models.ComandaService> ComandaService { get; set; } = default!;
        public DbSet<Management_Atelier_Reparatii.Models.Masina> Masina { get; set; } = default!;
        public DbSet<Management_Atelier_Reparatii.Models.Mecanic> Mecanic { get; set; } = default!;
    }
}
