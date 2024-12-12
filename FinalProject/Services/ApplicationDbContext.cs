using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinalProject.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(w => w.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Spell> Spells => Set<Spell>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<CharacterSpell> CharacterSpells => Set<CharacterSpell>();
    }
}
