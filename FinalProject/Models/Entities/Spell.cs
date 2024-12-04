using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class Spell
    {
        [StringLength(64)]
        string Name { get; set; } = string.Empty;
        [StringLength(256)]
        string? Description { get; set; } = string.Empty;
        [StringLength(256)]
        string Components { get; set; } = string.Empty;
        [StringLength(64)]
        string? Type { get; set; } = string.Empty;
        int Level { get; set; }
    }
}
