using ConferenceApp.Context;
using ConferenceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly UserContext _context;
        //private static List<ConferenceUser> _conferenceUsers = new List<ConferenceUser>();
        public ConferenceController(UserContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }
        
        [HttpGet]
        public IActionResult Register(int id = 0)
        {
            if (id == 0)
                return View(new ConferenceUser());
            else
                return View(_context.Users.Find(id));
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserID,FirstName,LastName,Email,ConferenceType,Photo")] ConferenceUser conferenceUser)
        {
            if (ModelState.IsValid)
            {
                if (conferenceUser.UserID == 0)
                    _context.Add(conferenceUser);
                else
                    _context.Update(conferenceUser);
                await _context.SaveChangesAsync();
                //_conferenceUsers.Add(conferenceUser);
                return RedirectToAction(nameof(Register));
            }
            return View(conferenceUser);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
