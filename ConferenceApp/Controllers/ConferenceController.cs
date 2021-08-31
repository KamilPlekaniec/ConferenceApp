using ConferenceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Controllers
{
    public class ConferenceController : Controller
    {
        private static List<ConferenceUser> _conferenceUsers = new List<ConferenceUser>();
        public IActionResult Index()
        {
            return View(_conferenceUsers);
        }

        [HttpPost]
        public IActionResult Register(ConferenceUser conferenceUser)
        {
            if (ModelState.IsValid)
            {
                _conferenceUsers.Add(conferenceUser);
                return RedirectToAction(nameof(Register));
            }
            return View(conferenceUser);
        }
    }
}
