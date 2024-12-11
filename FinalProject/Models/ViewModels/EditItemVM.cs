using FinalProject.Models.Entities;

namespace FinalProject.Models.ViewModels
{
    public class EditItemVM
    {
        public Character? Character { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Value { get; set; }
        public int Weight { get; set; }
        public string Type { get; set; } = String.Empty;

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
