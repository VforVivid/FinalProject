using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class Character
    {
            public int Id { get; set; }
            [StringLength(128)]
            public string? FirstName { get; set; } = string.Empty;
            public int Level { get; set; }
            [StringLength(128)]
            public string Race { get; set; } = string.Empty;
            [StringLength(128)]
            public string Class { get; set; } = string.Empty;
            public int Strength { get; set; }
            public int Dexterity { get; set; }
            public int Charisma { get; set; }
            public int Constitution { get; set; }
            public int ArmorClass { get; set; }
            public int Wisdom { get; set; }
            public int Intelligence { get; set; }
            public int NumberOfItems { get; set; }
            public ICollection<CharacterSpell> CharacterSpells { get; set; } = new List<CharacterSpell>();
            public ICollection<Item> Items { get; set; } = new List<Item>();
     
    }
}
