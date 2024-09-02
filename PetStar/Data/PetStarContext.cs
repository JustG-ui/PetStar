using Microsoft.EntityFrameworkCore;
using PetStar.Models;

namespace PetStar.Data
{
    public class PetStarContext : DbContext
    {
        public PetStarContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Animal> Animais { get; set; }

    }
}
