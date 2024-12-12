using FinalProject.Models.Entities;

namespace FinalProject.Services
{
    public interface ICharacterSpellRepository
    {
        Task<CharacterSpell> ReadAsync(int id);
        Task<ICollection<CharacterSpell>> ReadAllAsync();
        Task<CharacterSpell?> CreateAsync(int id, int spellId);
        Task UpdateCharacterSpellAsync(int characterSpellId, Spell spell);
        Task RemoveAsync(int characterId, int spellId);
    }
}
