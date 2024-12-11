using FinalProject.Models.Entities;

namespace FinalProject.Models.ViewModels
{
    public class EditItemVM
    {
        public Character? Character { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Value { get; set; }
        public int Weight { get; set; }
        public string Type { get; set; } = string.Empty;

        public Item GetItemInstance()
        {
            return new Item
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Value = Value,
                Weight = Weight,
                Type = Type
            };
        }
    }
}
