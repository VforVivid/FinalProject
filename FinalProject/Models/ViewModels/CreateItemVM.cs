using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.ViewModels
{
    public class CreateItemVM
    {
        public Character? Character { get; set; }  
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; } = string.Empty;
        [StringLength(256)]
        public string? Description { get; set; } = string.Empty;
        public int Value { get; set; }
        public int? Weight { get; set; }
        [StringLength(128)]
        public string? Type { get; set; } = string.Empty;
        public int CharacterId { get; set; }

        public Item GetItemInstance()
        {
            return new Item
            {
                Id = 0,
                Name = this.Name,
                Description = this.Description,
                Value = this.Value,
                Weight = this.Weight,
                Type = this.Type,
                CharacterId = this.CharacterId,
            };
        }
    }
}
