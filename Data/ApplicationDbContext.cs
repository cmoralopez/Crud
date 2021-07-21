using Crud.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crud.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Usuario>(en =>
            {
                en.Haskey(e => e.Codigo);

                en.Property(e => e.Nombre)
                //.IsRequeried()
                .HasMaxLength(100)
                .IsUnicode(false);

                en.Property(e => e.Apellido)
                //.IsRequeried()
                .HasMaxLength(100)
                .IsUnicode(false);

                en.Property(e => e.Direccion)
                //.IsRequeried()
                .HasMaxLength(250)
                .IsUnicode(false);
            });
        }
    }
}
