using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using UniBookHub.Data;
using UniBookHub.Models;



namespace UniBookHub.Controllers
{
    public class CollegeController : Controller
    {
        private readonly AppDbContext _context;

        public CollegeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: College
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colleges.Include(c=>c.Courses).ToListAsync());
        }

        // GET: College/Details/5
        public async Task<IActionResult> Details(int? CollegeId,int LevelId)
        { 
            var courses = _context.Courses
                .Include(c=>c.College)
                .Where(c =>( c.CollegeId == CollegeId && c.LevelNumber == LevelId)).ToList();
            College college = _context.Colleges.FirstOrDefault(c=>c.ID == CollegeId);
            TempData["Levels"] = college.NumberOfLevels;
            TempData["CollegeId"] = CollegeId;
            TempData["Level"] = LevelId;
            TempData["College"] = college.Name;
            return View(courses);
        }

        // GET: College/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: College/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(College college)
        {
            if (ModelState.IsValid)
            {
                _context.Add(college);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(college);
        }

        // GET: College/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colleges == null)
            {
                return NotFound();
            }

            var college = await _context.Colleges.FindAsync(id);
            if (college == null)
            {
                return NotFound();
            }
            return View(college);
        }

        // POST: College/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, College college)
        {
            if (id != college.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(college);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollegeExists(college.ID))
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
            return View(college);
        }

        // GET: College/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colleges == null)
            {
                return NotFound();
            }

            var college = await _context.Colleges
                .FirstOrDefaultAsync(m => m.ID == id);
            if (college == null)
            {
                return NotFound();
            }

            return View(college);
        }

        // POST: College/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Colleges == null)
            {
                return Problem("Entity set 'AppDbContext.Colleges'  is null.");
            }
            var college = await _context.Colleges.FindAsync(id);
            if (college != null)
            {
                _context.Colleges.Remove(college);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollegeExists(int id)
        {
          return (_context.Colleges?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
