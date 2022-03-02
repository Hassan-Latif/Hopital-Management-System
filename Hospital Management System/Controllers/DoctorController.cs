﻿using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorServices _doctorServices;
        private readonly ApplicationDbContext _context;
       
        public DoctorController(IDoctorServices doctorServices, ApplicationDbContext context)
        {
            _doctorServices = doctorServices;
            _context = context;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var doctor=from s in _context.Doctors select s;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctor = doctor.Where(s => s.Name.Contains(searchString)
                                       || s.Email.Contains(searchString));
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
                    doctor = doctor.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    doctor = doctor.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    doctor = doctor.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    doctor = doctor.OrderBy(s => s.CreatedDate);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Doctor>.CreateAsync(doctor.AsNoTracking(),pageNumber ?? 1,pageSize) );
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
