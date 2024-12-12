using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class CharacterSpellController : Controller
    {
        private readonly ICharacterSpellRepository _characterSpellRepo;
        private readonly ICharacterRepository _characterRepo;
        private readonly ISpellRepository _spellRepo;

        public CharacterSpellController(ICharacterSpellRepository characterSpellRepo, ICharacterRepository characterRepo, ISpellRepository spellRepo)
        {
            _characterSpellRepo = characterSpellRepo;
            _characterRepo = characterRepo;
            _spellRepo = spellRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Player, DM")]
        public async Task<IActionResult> Create([Bind(Prefix = "id")] int characterId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }

            var allSpells = await _spellRepo.ReadAllAsync();
            if (allSpells == null)
            {
                return RedirectToAction("Index", "Character");
            }

            var assignedSpells = character.CharacterSpells
                    .Select(cs => cs.Spell)
                        .ToList();

            var unassignedSpells = allSpells
                .Except(assignedSpells)
                    .ToList();

            var characterSpellVM = new CharacterSpellVM
            {
                Character = character,
                AvailableSpells = unassignedSpells,
            };
            return View(characterSpellVM);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(int characterId, int spellId)
        {
            await _characterSpellRepo.CreateAsync(characterId, spellId);
            return RedirectToAction("Details", "Character");
        }

        [Authorize(Roles = "Player, DM")]
        public async Task<IActionResult> Remove(int characterId, int spellId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }

            var spell = await _spellRepo.ReadAsync(spellId);
            if (spell == null)
            {
                return RedirectToAction("Details", "Character");
            }

            ViewData["Character"] = character;
            return View(spell);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmed(int characterId, int spellId)
        {
            await _characterSpellRepo.RemoveAsync(characterId, spellId);
            return RedirectToAction("Details", "Character");
        }
    }
}
