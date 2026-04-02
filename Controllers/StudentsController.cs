using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sepideh1.Data;
using Sepideh1.Models;

namespace Sepideh1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.AsNoTracking().ToListAsync();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student is null) return NotFound();

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create() => View();

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,DateOfBirth")] Student student)
        {
            if (!ModelState.IsValid) return View(student);

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student is null) return NotFound();

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,DateOfBirth")] Student student)
        {
            if (id != student.Id) return NotFound();

            if (!ModelState.IsValid) return View(student);

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExists(student.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student is null) return NotFound();

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is not null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudentExists(int id)
        {
            return await _context.Students.AnyAsync(e => e.Id == id);
        }
    }
}