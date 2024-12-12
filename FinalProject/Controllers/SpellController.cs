using FinalProject.Models.ViewModels;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class SpellController : Controller
    {
        private ICharacterRepository _characterRepo;
        private readonly ISpellRepository _spellRepo;

        public SpellController(ICharacterRepository characterRepo, ISpellRepository spellRepo)
        {
            _characterRepo = characterRepo;
            _spellRepo = spellRepo;
        }

        public async Task<IActionResult> Index()
        {
            var allSpells = await _spellRepo.ReadAllAsync();
            return View(allSpells);
        }

        //    public async Task<IActionResult> Learn(int characterId)
        //    {
        //        var character = await _characterRepo.ReadAsync(characterId);
        //        if (character == null)
        //        {
        //            return RedirectToAction("Details", "Character");
        //        }
        //        var spellVM = new CreateSpellVM
        //        {
        //            Character = character,
        //        };
        //        return View(spellVM);
        //    }

        //    [HttpPost, ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Learn(int characterId, CreateSpellVM spellVM)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var spell = spellVM.GetSpellInstance();
        //            await _characterRepo.CreateSpellAsync(characterId, spell);
        //            return RedirectToAction("Details", "Character");
        //        }
        //        itemVM.Character = await _characterRepo.ReadAsync(characterId);
        //        return View(itemVM);
        //    }
        //}

    }
}
