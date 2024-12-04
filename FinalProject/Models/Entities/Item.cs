using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class Item
    {
        [StringLength(128)]
        public string Name { get; set; } = string.Empty;
        [StringLength(256)]
        public string? Description { get; set; } = string.Empty;
        public int Value { get; set; }
        public int? Weight { get; set; }
        [StringLength(128)]
        public string? Type { get; set; } = string.Empty;
        public int CharacterId { get; set; }

    }
}
