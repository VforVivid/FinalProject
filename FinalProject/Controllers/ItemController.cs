using FinalProject.Models.Entities;
using FinalProject.Models.ViewModels;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace FinalProject.Controllers
{
    public class ItemController : Controller
    {
        private readonly ICharacterRepository _characterRepo;

        public ItemController(ICharacterRepository characterRepo)
        {
            _characterRepo = characterRepo;
        }

        [Authorize(Roles = "Player, DMs")]
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


        public async Task<IActionResult> Edit([Bind(Prefix = "id")] int characterId, int itemId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }
            var item = character.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return RedirectToAction(
                    "Details", "Character", new { id = characterId });
            }
            var model = new EditItemVM
            {
                Character = character,
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Value = item.Value,
                Weight = item.Weight,
                Type = item.Type
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int characterId, EditItemVM itemVM)
        {
            if (ModelState.IsValid)
            {
                var item = itemVM.GetItemInstance();
                await _characterRepo.UpdateItemAsync(characterId, item);
                return RedirectToAction("Details", "Character", new { id = characterId });
            }
            itemVM.Character = await _characterRepo.ReadAsync(characterId);
            return View(itemVM);
        }

        public async Task<IActionResult> Delete([Bind(Prefix = "id")] int characterId, int itemId)
        {
            var character = await _characterRepo.ReadAsync(characterId);
            if (character == null)
            {
                return RedirectToAction("Index", "Character");
            }
            var item = character.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return RedirectToAction(
                    "Details", "Character");
            }
            var model = new DeleteItemVM
            {
                Character = character,
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Weight = item.Weight,
                Value = item.Value,
                Type = item.Type
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int characterId)
        {
            await _characterRepo.DeleteItemAsync(characterId, id);
            return RedirectToAction("Details", "Character");
        }

    }


}

