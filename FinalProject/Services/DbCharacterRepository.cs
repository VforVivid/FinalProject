using FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalProject.Services
{
    public class DbCharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _db;

        public DbCharacterRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Character>> ReadAllAsync()
        {
            return await _db.Characters.ToListAsync();
        }

        public async Task<Character> CreateAsync(Character newCharacter)
        {
            await _db.Characters.AddAsync(newCharacter);
            await _db.SaveChangesAsync();
            return newCharacter;
        }

        public async Task<Character?> ReadAsync(int id)
        {
            return await _db.Characters.FindAsync(id);
            //return await _db.People.FirstOrDefaultAsync((p) => p.Id == id);
        }

        public async Task UpdateAsync(int oldId, Character character)
        {
            Character? characterToUpdate = await ReadAsync(oldId);
            if (characterToUpdate != null)
            {
                characterToUpdate.FirstName = character.FirstName;
                characterToUpdate.Level = character.Level;
                characterToUpdate.Class = character.Class;
                characterToUpdate.ArmorClass = character.ArmorClass;
                characterToUpdate.Intelligence = character.Intelligence;
                characterToUpdate.Dexterity = character.Dexterity;               
                characterToUpdate.Wisdom = character.Wisdom;
                characterToUpdate.Constitution = character.Constitution;
                characterToUpdate.Strength = character.Strength;
                characterToUpdate.Items = character.Items;
                characterToUpdate.CharacterSpells = character.CharacterSpells;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Character? characterToDelete = await ReadAsync(id);
            if (characterToDelete != null)
            {
                _db.Characters.Remove(characterToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Item> CreateItemAsync(int characterId, Item item)
        {
            var character = await ReadAsync(characterId);
            if (character != null)
            {
                character.Items.Add(item);
                item.CharacterId = character.Id;
                await _db.SaveChangesAsync();
            }
            return item;
        }

    }
}
