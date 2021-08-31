using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Controllers
{
    public class ConferenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
