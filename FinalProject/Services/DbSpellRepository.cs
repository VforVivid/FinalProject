using FinalProject.Models.Entities;
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
    }
}
