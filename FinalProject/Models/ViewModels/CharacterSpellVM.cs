using FinalProject.Models.Entities;

namespace FinalProject.Models.ViewModels
{
    public class CharacterSpellVM
    {
        public Character? Character { get; set; }
        public List<Spell> AvailableSpells { get; set; } = new List<Spell>();
    }
}
