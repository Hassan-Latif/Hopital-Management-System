using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorServices _doctorServices;

        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _doctorServices.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorViewModel doctor)
        {
            if (ModelState.IsValid)
            {
                await _doctorServices.AddAsync(doctor);
                return RedirectToAction("Index");
            }
            return View(doctor);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var doctor = await _doctorServices.GetByIdAsync(id.GetValueOrDefault());
            if(doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,DoctorViewModel doctor)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _doctorServices.UpdateAsync(doctor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _doctorServices.GetByIdAsync(doctor.DoctorId) == null)
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
            return View(doctor);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _doctorServices.GetByIdAsync(id.GetValueOrDefault());
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorServices.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _doctorServices.GetByIdAsync(id.GetValueOrDefault());
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

    }
}
