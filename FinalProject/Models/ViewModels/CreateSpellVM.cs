using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.ViewModels
{
    public class CreateSpellVM
    {
        public Spell? Spell { get; set; }
        public Character? Character { get; set; }
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

        public Spell GetSpellInstance()
        {
            return new Spell
            {
                Id = 0,
                Name = this.Name,
                Description = this.Description,
                Components = this.Components,
                Type = this.Type,
                Level = this.Level
            };
        }
    }
}
