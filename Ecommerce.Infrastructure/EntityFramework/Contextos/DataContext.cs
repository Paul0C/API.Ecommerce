using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Identity;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.EntityFramework.Contextos
{
    public class DataContext : IdentityDbContext<User, Role, int, 
                                                 IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                 IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItems { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                        .HasKey(ur => new{ur.UserId, ur.RoleId});

            modelBuilder.Entity<UserRole>()
                        .HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
            
            modelBuilder.Entity<UserRole>()
                        .HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

            modelBuilder.Entity<Produto>()
                        .HasOne(c => c.Categoria)
                        .WithMany(p => p.Produtos)
                        .IsRequired();

            modelBuilder.Entity<Produto>()
                        .HasOne(c => c.Marca)
                        .WithMany(p => p.Produtos)
                        .IsRequired();
            
            modelBuilder.Entity<Carrinho>()
                        .HasMany(c => c.Itens)
                        .WithOne(i => i.Carrinho)
                        .IsRequired();
            
            modelBuilder.Entity<CarrinhoItem>()
                        .HasOne(i => i.Produto)
                        .WithMany(p => p.Itens)
                        .IsRequired();
        }
    }
}