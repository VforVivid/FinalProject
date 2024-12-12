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
            return await _db.Characters
                .Include(c => c.Items)
                .Include(cs => cs.CharacterSpells)
                    .ThenInclude(s => s.Spell)
                .ToListAsync();
        }

        public async Task<Character> CreateAsync(Character newCharacter)
        {
            await _db.Characters.AddAsync(newCharacter);
            await _db.SaveChangesAsync();
            return newCharacter;
        }

        public async Task<Character?> ReadAsync(int id)
        {
            var character = await _db.Characters.FindAsync(id);
            if (character != null)
            {
                _db.Entry(character).Collection(p => p.Items).Load();
            }
            return await _db.Characters
                .Include(cs => cs.CharacterSpells)
                    .ThenInclude(s => s.Spell)
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);
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
                item.Character = character;
                await _db.SaveChangesAsync();
            }
            return item;
        }

        public async Task UpdateItemAsync(int characterId, Item item)
        {
            var character = await ReadAsync(characterId);
            if (character != null)
            {
                var itemToUpdate = character.Items.FirstOrDefault(i => i.Id == item.Id);
                if (itemToUpdate != null)
                {

                    itemToUpdate.Name = item.Name;
                    itemToUpdate.Description = item.Description;
                    itemToUpdate.Value = item.Value;
                    itemToUpdate.Weight = item.Weight;
                    itemToUpdate.Type = item.Type;
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteItemAsync(int characterId, int itemId)
        {
            var character = await ReadAsync(characterId);
            if (character != null)
            {
                var item = character.Items
                    .FirstOrDefault(i => i.Id == itemId);
                if (item != null)
                {
                    character.Items.Remove(item);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
