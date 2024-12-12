using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class DbSpellRepository : ISpellRepository
    {
        private readonly ApplicationDbContext _db;

        public DbSpellRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Spell?> ReadAsync(int id)
        {
            return await _db.Spells
                .Include(cs => cs.SpellsCharacter)
                    .ThenInclude(s => s.Spell)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Spell>> ReadAllAsync()
        {
            return await _db.Spells
                .Include(cs => cs.SpellsCharacter)
                    .ThenInclude(s => s.Spell)
                .ToListAsync();
        }

        public async Task<Spell> CreateAsync(Spell newSpell)
        {
            await _db.Spells.AddAsync(newSpell);
            await _db.SaveChangesAsync();
            return newSpell;
        }

        public async Task UpdateAsync(Spell updatedSpell)
        {
            var existingSpell = await ReadAsync(updatedSpell.Id);
            if (existingSpell != null)
            {
                existingSpell.Name = updatedSpell.Name;
                existingSpell.Description = updatedSpell.Description;
                existingSpell.Components = updatedSpell.Components;
                existingSpell.Type = updatedSpell.Type;
                existingSpell.Level = updatedSpell.Level;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var spell = await ReadAsync(id);
            if (spell != null)
            {
                _db.Spells.Remove(spell);
                await _db.SaveChangesAsync();
            }
        }
    }
}
