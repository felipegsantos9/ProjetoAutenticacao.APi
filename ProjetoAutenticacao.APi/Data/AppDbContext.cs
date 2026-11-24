using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjetoAutenticacao.APi.Models;

namespace ProjetoAutenticacao.Api{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Passageiro> Passageiros { get; set; }
        public DbSet<Passagem> Passagens { get; set; }
    }
}