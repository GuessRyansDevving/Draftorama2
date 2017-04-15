using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Draftorama.Models;

namespace Draftorama.Controllers
{
    public class HomeController : Controller
    {
        #region Public Methods

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            Set s = new Set("MM3");
            //s.ImportFromTXT();
            //s.ExportToJSON();
            s.ImportFromJSON();
            Pack p = new Pack(s);

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Intro()
        {
            return View();
        }

        #endregion Public Methods
    }
}