using Crud.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crud.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRoles, string>
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
                en.HasKey(e => e.Codigo);

                en.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                en.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                en.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

                en.Property(e => e.Estado)
                .IsRequired()
                .IsUnicode(false);
            });
        }
    }
}
