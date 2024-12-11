using FinalProject.Models.Entities;
using System.Net.Http.Headers;

namespace FinalProject.Models.ViewModels
{
    public class DeleteItemVM
    {
        public Character? Character { get; set; }
        public int ? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Value { get; set; }
        public int Weight { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
