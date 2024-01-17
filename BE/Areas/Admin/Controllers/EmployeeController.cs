using BE.Areas.Admin.ViewModels;
using BE.DAL;
using BE.Models;
using BE.Utilities.Extentions;
using BE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return BadRequest();
            double count = await _context.Employees.CountAsync();
            ICollection<Employee> items = await _context.Employees.Skip((page - 1) * 4).Take(4)
                .Include(x => x.Position).ToListAsync();
            PaginationVM<Employee> vM = new PaginationVM<Employee>
            {
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / 4),
                Items = items
            };
            return View(vM);
        }
        public async Task<IActionResult> Create()
        {
            CreateEmployeeVM create = new CreateEmployeeVM
            {
                Positions = await _context.Positions.ToListAsync()
            };
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM create)
        {
            if (!ModelState.IsValid)
            {
                create.Positions = await _context.Positions.ToListAsync();
                return View(create);
            }
            bool result = await _context.Employees.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower());
            if (result)
            {
                create.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }
            if (!create.Photo.IsValid())
            {
                create.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "Is not valid");
                return View(create);
            }
            if (!create.Photo.LimiSize())
            {
                create.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "Limit sixe is  10MB");
                return View(create);
            }
            Employee item = new Employee
            {
                Name = create.Name,
                Surname = create.Surname,
                Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img"),
                FaceLink = create.TwitLink,
                TwitLink = create.TwitLink,
                GoogleLink = create.GoogleLink,
                PositionId = create.PositionId
            };

            await _context.Employees.AddAsync(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Employee item = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            UpdateEmployeeVM update = new UpdateEmployeeVM
            {
                Name = item.Name,
                Surname = item.Surname,
                Img = item.Img,
                FaceLink = item.TwitLink,
                TwitLink = item.TwitLink,
                GoogleLink = item.GoogleLink,
                PositionId = item.PositionId,
                Positions = await _context.Positions.ToListAsync()
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateEmployeeVM update)
        {
            if (!ModelState.IsValid)
            {
                update.Positions = await _context.Positions.ToListAsync();
                return View(update);
            }
            Employee item = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            bool result = await _context.Employees.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id);
            if (result)
            {
                update.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }
            if (update.Photo != null)
            {
                if (!update.Photo.IsValid())
                {
                    update.Positions = await _context.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "Is not valid");
                    return View(update);
                }
                if (!update.Photo.LimiSize())
                {
                    update.Positions = await _context.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "Limit sixe is  10MB");
                    return View(update);
                }
                item.Img.DeleteAsync(_env.WebRootPath, "assets", "img");
                item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }
            item.Name = update.Name;
            item.Surname = item.Surname;
            item.FaceLink = item.TwitLink;
            item.TwitLink = item.TwitLink;
            item.GoogleLink = item.GoogleLink;
            item.PositionId = item.PositionId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Employee item = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            item.Img.DeleteAsync(_env.WebRootPath, "assets", "img");
            _context.Employees.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
