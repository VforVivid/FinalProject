using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class Spell
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string Name { get; set; } = string.Empty;
        [StringLength(256)]
        public string? Description { get; set; } = string.Empty;
        [StringLength(256)]
        public string Components { get; set; } = string.Empty;
        [StringLength(64)]
        public string? Type { get; set; } = string.Empty;
        public int Level { get; set; }
        public ICollection<CharacterSpell> SpellsCharacter { get; set; } = new List<CharacterSpell>();
    }
}
