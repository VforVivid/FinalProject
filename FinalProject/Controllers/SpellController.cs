using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "DM")]
        public IActionResult Create()
        {
            return View(new Spell());
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spell spell)
        {
            if (ModelState.IsValid)
            {
                await _spellRepo.CreateAsync(spell);
                return RedirectToAction(nameof(Index)); 
            }
            return View(spell);
        }

        [Authorize(Roles = "DM")]
        public async Task<IActionResult> Edit(int id)
        {
            var spell = await _spellRepo.ReadAsync(id);
            if (spell == null)
            {
                return NotFound();
            }
            return View(spell);
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Spell spell)
        {
            if (ModelState.IsValid)
            {
                await _spellRepo.UpdateAsync(spell);
                return RedirectToAction(nameof(Index));
            }
            return View(spell);
        }

        [Authorize(Roles = "DM")]
        public async Task<IActionResult> Delete(int id)
        {
            var spell = await _spellRepo.ReadAsync(id);
            if (spell == null)
            {
                return NotFound();
            }
            return View(spell);
        }

        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _spellRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}

