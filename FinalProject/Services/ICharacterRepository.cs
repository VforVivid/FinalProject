using System;
using FinalProject.Models.Entities;

namespace FinalProject.Services
{
    public interface ICharacterRepository
    {
        Task<ICollection<Character>> ReadAllAsync();
        Task<Character> CreateAsync(Character newCharacter);
        Task<Character?> ReadAsync(int id);
        Task UpdateAsync(int oldId, Character character);
        Task DeleteAsync(int id);
        Task<Item> CreateItemAsync(int characterId, Item item);
        Task UpdateItemAsync(int characterId, Item item);
        Task DeleteItemAsync(int characterId, int itemId);  
    }
}
