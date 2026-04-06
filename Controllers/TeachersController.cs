using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sepideh1.Data;
using Sepideh1.Models;

namespace Sepideh1.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers.AsNoTracking().ToListAsync();
            return View(teachers);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var teacher = await _context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher is null) return NotFound();

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create() => View();

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,HireDate")] Teacher teacher)
        {
            if (!ModelState.IsValid) return View(teacher);

            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher is null) return NotFound();

            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,HireDate")] Teacher teacher)
        {
            if (id != teacher.Id) return NotFound();

            if (!ModelState.IsValid) return View(teacher);

            try
            {
                _context.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TeacherExists(teacher.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var teacher = await _context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher is null) return NotFound();

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher is not null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TeacherExists(int id)
        {
            return await _context.Teachers.AnyAsync(e => e.Id == id);
        }
    }
}
