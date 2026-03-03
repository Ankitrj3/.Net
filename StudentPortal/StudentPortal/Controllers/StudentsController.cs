using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Models;
using StudentPortal.Service;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentPortalDbContext _context;
        private readonly IStudentService _service; // Dependency Injection for the service layer

        public StudentsController(StudentPortalDbContext context, IStudentService service)// it is unity framework
        {
            _context = context;
            _service = service;
        }

        // GET: Students
        public async Task<IActionResult> Index(string q = null)
        {
            var items = await _service.SearchAsync(q);
            return View(items);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FullName,Email,Phone,Status,JoinDate,CreatedAt")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _service.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FullName,Email,Phone,Status,JoinDate,CreatedAt")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateStudentAsync(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await StudentExistsAsync(student.StudentId))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudentExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(e => e.StudentId == id);
        }
    }
}