using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proyectofinal.Models;
using proyectofinal.Models.AccountViewModels;

namespace proyectofinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<proyectofinal.Models.AccountViewModels.RegisterViewModel> RegisterViewModel { get; set; }
        public DbSet<proyectofinal.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<IdentityRole> identityRole { get; set; }
    }
}
