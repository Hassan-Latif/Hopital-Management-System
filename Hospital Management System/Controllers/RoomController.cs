using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomServices _roomServices;
        private readonly ApplicationDbContext _context;
        public RoomController(IRoomServices roomServices, ApplicationDbContext context)
        {
            _roomServices = roomServices;
            _context = context;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var room = from s in _context.Rooms select s;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            if (!String.IsNullOrEmpty(searchString))
            {
                room = room.Where(s => s.RoomName.Contains(searchString));

            }
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    room = room.OrderByDescending(s => s.RoomName);
                    break;
                case "Date":
                    room = room.OrderBy(s => s.RoomDate);
                    break;
                case "date_desc":
                    room = room.OrderByDescending(s => s.RoomDate);
                    break;
                case "roomType_desc":
                    room = room.OrderByDescending(s => s.RoomTypeName);
                    break;
                case "capacity_desc":
                    room = room.OrderByDescending(s => s.RoomCapacity);
                    break;
                default:
                    room = room.OrderBy(s => s.RoomDate);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Rooms>.CreateAsync(room.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.RoomsTypeId = _context.RoomTypes.Select(x => new SelectListItem
            {
                Text = x.RoomTypeName,
                Value = x.RoomTypeId.ToString()
            }).ToList();
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomsViewModel room)
        {
            ViewBag.RoomsTypeId = _context.RoomTypes.Select(x => new SelectListItem
            {
                Text = x.RoomTypeName,
                Value = x.RoomTypeId.ToString()
            }).ToList();
            if (ModelState.IsValid)
            {
                await _roomServices.AddAsync(room);
                return RedirectToAction("Index");
            }
            return View(room);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var room = await _roomServices.GetByIdAsync(id.GetValueOrDefault());
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RoomsViewModel room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _roomServices.UpdateAsync(room);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _roomServices.GetByIdAsync(room.RoomId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomServices.GetByIdAsync(id.GetValueOrDefault());
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomServices.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var room = await _roomServices.GetByIdAsync(id.GetValueOrDefault());
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
    }
}
