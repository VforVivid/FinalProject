using FinalProject.Models.Entities;

namespace FinalProject.Models.ViewModels
{
    public class CharacterDetailsVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public int Level { get; set; }
        public string Race { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Charisma { get; set; }
        public int Constitution { get; set; }
        public int ArmorClass { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

    }
}
