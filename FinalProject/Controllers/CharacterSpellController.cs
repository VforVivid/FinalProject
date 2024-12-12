using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using FinalProject.Services;
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

        public async Task<IActionResult> Create([Bind(Prefix = "id")] int id, int spellId)
        {
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }

            var spell = await _spellRepo.ReadAsync(spellId);
            if (spell == null)
            {
                return RedirectToAction("Details", "Character");
            }

            var characterSpell = character.CharacterSpells
                .SingleOrDefault(c => c.SpellId == spellId);
            if (characterSpell == null)
            {
                return RedirectToAction("Details", "Character");
            }

            var characterSpellVM = new CharacterSpellVM
            {
                Character = character,
                Spell = spell,
            };
            return View(characterSpellVM);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(int id, int spellId)
        {
            await _characterSpellRepo.CreateAsync(id, spellId);
            return RedirectToAction("Details", "Character");
        }

        public async Task<IActionResult> LearnSpell([Bind(Prefix = "id")] int id, int spellId)
        {
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }
            var characterSpell = character.CharacterSpells
                .FirstOrDefault(s => s.SpellId == spellId);
            if(characterSpell == null)
            {
                RedirectToAction("Details", "Character");
            }
            return View(characterSpell);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("LearnSpell")]
        public async Task<IActionResult> LearnSpellConfirmed(int id, int characterSpellId, Spell spell)
        {
            await _characterSpellRepo.UpdateCharacterSpellAsync(characterSpellId, spell);
            return RedirectToAction("Details", "Character");
        }

        public async Task<IActionResult> Remove(int id, int spellId)
        {
            var character = await _characterRepo.ReadAsync(id);
            if(character == null)
            {
                return RedirectToAction("Details", "Character");
            }
            var characterSpell = character.CharacterSpells
                .FirstOrDefault(s => s.SpellId == spellId);
            if (characterSpell == null)
            {
                return RedirectToAction("Details", "Character");
            }
            return View(characterSpell);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmed(int id, int characterSpellId)
        {
            await _characterSpellRepo.RemoveAsync(id, characterSpellId);
            return RedirectToAction("Details", "Character");
        }
    }
}
