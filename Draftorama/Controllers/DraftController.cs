using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Draftorama.Models;

namespace Draftorama.Controllers
{
    public class DraftController : Controller
    {
        public IActionResult Draft()
        {
            ViewData["Message"] = "The Draft Page.";

            Set s = new Set("MM3");
            s.ImportFromJSON();
            Pack p = new Pack(s);

            return View(p.CardsInPack);
        }
    }
}