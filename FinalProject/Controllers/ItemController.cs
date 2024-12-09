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

        public IActionResult Index()
        {
            return View();
        }
    }
}
