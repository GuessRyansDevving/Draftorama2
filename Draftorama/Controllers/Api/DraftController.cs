using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Draftorama.Models;
using Draftorama.ViewModels;
using AutoMapper;

namespace Draftorama.Controllers
{
    [Route("api/drafts")]
    public class DraftController : Controller
    {
        #region Private Fields

        private IDraftRepository _repository;

        #endregion Private Fields

        #region Public Methods

        public async Task<IActionResult> Draft(string setName)
        {
            ViewData["Title"] = setName;

            Set s = new Set(setName);
            s.ImportFromJSON();
            Draft d = new Draft(s);

            await SaveDraft(d.DraftName, d);
            return View("Draft", d);
        }

        public Draft GetDraft(string draftName)
        {
            return _repository.GetDraftByName(draftName);
        }

        public IActionResult Intro()
        {
            return View();
        }

        public async Task<IActionResult> MakePick(string draftName, string cardPickedNameIn)
        {
            Draft d = GetDraft(draftName);
            d.Table.First().MakePick(cardPickedNameIn);

            await SaveDraft(draftName, d);
            return View("Draft", d);
        }

        public async Task<IActionResult> MovePickToDeck(string draftName, string cardClickedIn)
        {
            Draft d = GetDraft(draftName);
            Card c = d.Table.First().CardPool.PicksNotInDeck.First(a => a.CardName == cardClickedIn);
            d.Table.First().CardPool.PicksInDeck.Add(c);
            d.Table.First().CardPool.PicksNotInDeck.Remove(c);

            await SaveDraft(draftName, d);
            return View("Draft", d);
        }

        public async Task<IActionResult> RemovePickFromDeck(string draftName, string cardClickedIn)
        {
            Draft d = GetDraft(draftName);
            Card c = d.Table.First().CardPool.PicksInDeck.First(a => a.CardName == cardClickedIn);
            d.Table.First().CardPool.PicksNotInDeck.Add(c);
            d.Table.First().CardPool.PicksInDeck.Remove(c);

            await SaveDraft(draftName, d);
            return View("Draft", d);
        }

        public async Task<IActionResult> SaveDraft(string draftName, Draft d)
        {
            if (ModelState.IsValid)
            {
                // Save to the Database
                _repository.AddDraft(d);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/drafts/{d.DraftName}", Mapper.Map<DraftViewModel>(d));
                }
                else
                {
                    return BadRequest("Failed to save changes to the database");
                }
            }

            return BadRequest(ModelState);
        }

        #endregion Public Methods
    }
}