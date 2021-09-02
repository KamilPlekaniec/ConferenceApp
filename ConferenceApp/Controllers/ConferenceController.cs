using ConferenceApp.Context;
using ConferenceApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Controllers
{
    public class ConferenceController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly UserContext _context;
        public ConferenceController(IWebHostEnvironment environment, UserContext context)
        {
            _environment = environment;
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
                var imageName = HttpContext.Session.GetString("ImageName");
                if (!string.IsNullOrEmpty(imageName))
                {
                    conferenceUser.Photo = imageName;
                    HttpContext.Session.SetString("ImageName", "");
                }
                if (conferenceUser.UserID == 0)
                    _context.Add(conferenceUser);
                else
                    _context.Update(conferenceUser);
                await _context.SaveChangesAsync();
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

        public async Task UploadFile(IFormFile file)
        {
            if (file == null)
            {
                HttpContext.Session.SetString("ImageName", "");
                return;
            }

            var uniqueName = $"{Guid.NewGuid()}_{file.FileName}";
            HttpContext.Session.SetString("ImageName", uniqueName);

            var toFolder = Path.Combine(_environment.WebRootPath, "images");
            var filePath = Path.Combine(toFolder, uniqueName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
