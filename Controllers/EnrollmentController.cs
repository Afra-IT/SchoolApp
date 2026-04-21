using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sepideh1.Data;
using Sepideh1.Models;

namespace Sepideh1.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Enrollments/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int studentId, int lessonId)
        {
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson is null) return NotFound();

            // current enrolled count
            var currentCount = await _context.Enrollments
                .CountAsync(e => e.LessonId == lessonId);

            if (currentCount >= lesson.Capacity)
            {
                ModelState.AddModelError(string.Empty, "Lesson is full.");
                // Return to student details or show error page; here redirect back
                return RedirectToAction("Details", "Students", new { id = studentId });
            }

            // prevent duplicate
            var exists = await _context.Enrollments
                .AnyAsync(e => e.StudentId == studentId && e.LessonId == lessonId);

            if (!exists)
            {
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    LessonId = lessonId
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Students", new { id = studentId });
        }

        // POST: Enrollments/Unregister
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unregister(int studentId, int lessonId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.LessonId == lessonId);

            if (enrollment is not null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Students", new { id = studentId });
        }
    }
}