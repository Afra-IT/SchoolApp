using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sepideh1.Data;
using Sepideh1.Models;

namespace Sepideh1.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.AsNoTracking().ToListAsync();
            return View(rooms);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var room = await _context.Rooms
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (room is null) return NotFound();

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create() => View();

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,Description,Capacity,IsAvailable")] Room room)
        {
            if (!ModelState.IsValid) return View(room);

            _context.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var room = await _context.Rooms.FindAsync(id);
            if (room is null) return NotFound();

            return View(room);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description,Capacity,IsAvailable")] Room room)
        {
            if (id != room.Id) return NotFound();

            if (!ModelState.IsValid) return View(room);

            try
            {
                _context.Update(room);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists(room.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var room = await _context.Rooms
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (room is null) return NotFound();

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room is not null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoomExists(int id)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == id);
        }
    }
}
