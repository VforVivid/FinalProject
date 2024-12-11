using Microsoft.AspNetCore.Mvc;
using FinalProject.Services;
using FinalProject.Models.Entities;
using System;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    public class CharacterController : Controller
    {

        private readonly ICharacterRepository _characterRepo;

        public CharacterController(ICharacterRepository characterRepo)
        {
            _characterRepo = characterRepo;
        }
        public async Task<IActionResult> Index()
        {
            var allCharacter = await _characterRepo.ReadAllAsync();
            var model = allCharacter.Select(c =>
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
                    Items = c.Items,
                    NumberOfItems = c.Items.Count
            });
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Character newCharacter)
        {
            if (ModelState.IsValid)
            {
                await _characterRepo.CreateAsync(newCharacter);
                return RedirectToAction("Index");
            }
            return View(newCharacter);
        }

        public async Task<IActionResult> Details(int id)
        {
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index");
            }
            return View(character);
        }

        [Authorize(Roles = "Player, DMs")]
        public async Task<IActionResult> Edit(int id)
        {
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index");
            }
            return View(character);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Character character)
        {
            if (ModelState.IsValid)
            {
                await _characterRepo.UpdateAsync(character.Id, character);
                return RedirectToAction("Index");
            }
            return View(character);
        }
        [Authorize(Roles = "Player, DM")]
        public async Task<IActionResult> Delete(int id)
        {
            var character = await _characterRepo.ReadAsync(id);
            if (character == null)
            {
                return RedirectToAction("Index");
            }
            return View(character);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _characterRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
