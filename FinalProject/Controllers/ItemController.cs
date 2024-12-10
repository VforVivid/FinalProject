using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ItemController : Controller
    {
        private readonly ICharacterRepository _characterRepo;

        public ItemController(ICharacterRepository characterRepo)
        {
            _characterRepo = characterRepo;
        }

        public async Task<IActionResult> Create([Bind(Prefix = "id")] int characterId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }
            var itemVM = new CreateItemVM
            {
                Character = character
            };
            return View(itemVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int characterId, CreateItemVM itemVM)
        {
            if (ModelState.IsValid)
            {
                var item = itemVM.GetItemInstance();
                await _characterRepo.CreateItemAsync(characterId, item);
                return RedirectToAction("Index", "Character");
            }
            itemVM.Character = await _characterRepo.ReadAsync(characterId);
            return View(itemVM);
        }

        public async Task<IActionResult> Index()
        {
            var allCharacters = await _characterRepo.ReadAllAsync();
            var model = allCharacters.Select(c =>
            new CharacterDetailsVM
            {
                Id = c.Id,
                FirstName = c.FirstName,
                Level = c.Level,
                Race = c.Race,
                Class = c.Class,
                ArmorClass = c.ArmorClass,
                Strength = c.Strength,
                Dexterity = c.Dexterity,
                Charisma = c.Charisma,
                Constitution = c.Constitution,
                Wisdom = c.Wisdom,
                Intelligence = c.Intelligence,
                NumberOfItems = c.Items.Count
            });
            return View(model);
        }
    }
}
