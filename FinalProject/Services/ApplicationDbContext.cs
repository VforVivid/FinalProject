using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models.Entities;

namespace FinalProject.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Spell> Spells => Set<Spell>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<CharacterSpell> CharacterSpells => Set<CharacterSpell>();
    }
}
