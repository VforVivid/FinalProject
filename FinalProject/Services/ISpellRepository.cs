using FinalProject.Models.Entities;

namespace FinalProject.Services
{
    public interface ISpellRepository
    {
        Task<Spell?> ReadAsync(int Id);
        Task<ICollection<Spell>> ReadAllAsync();
    }
}
