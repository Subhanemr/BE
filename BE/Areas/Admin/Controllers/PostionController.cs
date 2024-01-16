﻿using BE.Areas.Admin.ViewModels;
using BE.DAL;
using BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class PostionController : Controller
    {
        private readonly AppDbContext _context;

        public PostionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Position> items = await _context.Positions.Include(x => x.Employees).ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVM create)
        {
            if (!ModelState.IsValid) return View(create);

            bool result = await _context.Positions.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }
            Position item = new Position { Name = create.Name };

            await _context.Positions.AddAsync(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Position item = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            UpdatePositionVM update = new UpdatePositionVM { Name = item.Name };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePositionVM update)
        {
            if (!ModelState.IsValid) return View(update);
            Position item = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            bool result = await _context.Positions.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }
            item.Name = update.Name;

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Position item = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            _context.Positions.Remove(item);
            return RedirectToAction(nameof(Index));
        }
    }
}
