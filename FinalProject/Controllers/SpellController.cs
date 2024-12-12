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

        public async Task<IActionResult> Learn([Bind(Prefix = "id")] int characterId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }
            var allSpells = await _spellRepo.ReadAllAsync();
            var spellsLearned = character.CharacterSpells
                .Select(s => s.Spell).ToList();
            var spellsNotLearned = allSpells.Except(spellsLearned);
            ViewData["Character"] = character;
            return View(spellsNotLearned);
        }
    }
}
