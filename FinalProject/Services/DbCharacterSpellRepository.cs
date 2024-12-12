using FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class DbCharacterSpellRepository : ICharacterSpellRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ICharacterRepository _characterRepo;
        private readonly ISpellRepository _spellRepo;


        public DbCharacterSpellRepository(ApplicationDbContext db, ICharacterRepository characterRepo, ISpellRepository spellRepo)
        {
            _db = db;
            _characterRepo = characterRepo;
            _spellRepo = spellRepo;
        }

        public async Task<CharacterSpell?> ReadAsync(int id)
        {
            return await _db.CharacterSpells
                .Include(c => c.Character)
                    .ThenInclude(si => si!.Items)
                .Include(c => c.Spell)
                .FirstOrDefaultAsync(c => c.Id == id);
                    
        }

        public async Task<ICollection<CharacterSpell>> ReadAllAsync()
        {
            return await _db.CharacterSpells 
                .Include (c => c.Character)
                    .ThenInclude (si => si!.Items)
                .Include(cs => cs.Spell)
                .ToListAsync();
        }

        public async Task<CharacterSpell?> CreateAsync(int characterId, int spellId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return null;
            }
            var characterSpells = character.CharacterSpells
                .FirstOrDefault(cs => cs.SpellId == spellId);
            if (characterSpells != null)
            {
                return null;
            }
            var spell = await _spellRepo.ReadAsync(spellId);
            if (spell == null)
            {
                return null;
            }
            var characterSpell = new CharacterSpell
            {
                CharacterId = characterId,
                SpellId = spellId
            };
            character.CharacterSpells.Add(characterSpell);
            spell.SpellsCharacter.Add(characterSpell);
            await _db.SaveChangesAsync();
            return characterSpell;
        }

        public async Task UpdateCharacterSpellAsync(
            int characterSpellId, Spell spell)
        {
            var characterSpell = await ReadAsync(characterSpellId);
            if (characterSpell != null)
            {
                characterSpell.Spell = spell;
                await _db.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(int characterId, int spellId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            var characterSpell = character!.CharacterSpells
                .FirstOrDefault(sc => sc.SpellId == spellId);
            var spell = characterSpell!.Spell;
            character!.CharacterSpells.Remove(characterSpell);
            spell!.SpellsCharacter.Remove(characterSpell);
            await _db.SaveChangesAsync();
        }
    }
}

