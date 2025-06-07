using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseAlunos.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ModeloAluno> Alunos { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=BaseDeAlunos;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
    }
}