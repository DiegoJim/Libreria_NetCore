using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Libro> Libro { get; set; }
        public DbSet<editorial> Editorial { get; set; }
        public DbSet<autores> Autores { get; set; }
        public DbSet<autores_has_libros> Autores_Has_Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<autores_has_libros>().HasKey(x => new { x.autoresId, x.LibroId });
        }
    }
}
