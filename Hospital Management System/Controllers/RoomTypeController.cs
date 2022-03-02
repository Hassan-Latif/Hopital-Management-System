using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    [Authorize]
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeServices _roomTypeServices;
        private readonly ApplicationDbContext _context;
        public RoomTypeController(IRoomTypeServices roomTypeServices, ApplicationDbContext context)
        {
            _roomTypeServices = roomTypeServices;
            _context = context; 
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var roomType = from s in _context.RoomTypes select s;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            if (!String.IsNullOrEmpty(searchString))
            {
                roomType = roomType.Where(s => s.RoomTypeName.Contains(searchString));

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
                    roomType = roomType.OrderByDescending(s => s.RoomTypeName);
                    break;
                case "Date":
                    roomType = roomType.OrderBy(s => s.RoomTypeDateTime);
                    break;
                case "date_desc":
                    roomType = roomType.OrderByDescending(s => s.RoomTypeDateTime);
                    break;
                default:
                    roomType = roomType.OrderBy(s => s.RoomTypeDateTime);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<RoomType>.CreateAsync(roomType.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomTypeViewModel roomType)
        {
            if (ModelState.IsValid)
            {
                await _roomTypeServices.AddAsync(roomType);
                return RedirectToAction("Index");
            }
            return View(roomType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var roomType = await _roomTypeServices.GetByIdAsync(id.GetValueOrDefault());
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RoomTypeViewModel roomType)
        {
            if (id != roomType.RoomTypeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _roomTypeServices.UpdateAsync(roomType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _roomTypeServices.GetByIdAsync(roomType.RoomTypeId) == null)
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
            return View(roomType);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeServices.GetByIdAsync(id.GetValueOrDefault());
            if (roomType== null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomTypeServices.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var roomType = await _roomTypeServices.GetByIdAsync(id.GetValueOrDefault());
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

    }
}
