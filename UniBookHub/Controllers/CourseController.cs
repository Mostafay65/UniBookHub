using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniBookHub.Data;
using UniBookHub.Models;
using UniBookHub.ViewModels;

namespace UniBookHub.Controllers;

public class CourseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CourseController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Course
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Courses.Include(c => c.College);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Course/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var course = await _context.Courses
            .Include(c => c.College)
            .FirstOrDefaultAsync(m => m.ID == id);

        TempData["CollegeId"] = course.CollegeId;
        TempData["Level"] = course.LevelNumber;
        TempData["Levels"] = course.College.NumberOfLevels;
        TempData["College"] = course.College.Name;
        return View(course);
    }

    // GET: Course/Create
    public IActionResult Create(int CollegeId, int LevelId)
    {
        var college = _context.Colleges.FirstOrDefault(c => c.ID == CollegeId);
        TempData["CollegeId"] = CollegeId;
        TempData["Level"] = LevelId;
        TempData["Levels"] = college.NumberOfLevels;
        TempData["College"] = college.Name;
        return View();
    }

    // POST: Course/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseViewModel course, int LevelId)
    {
        if (ModelState.IsValid && course.Book is not null && course.Book.Length > 0)
        {
            var FileName = Guid.NewGuid().ToString() + '_' + course.Book.FileName;
            var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Books", FileName);

            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                await course.Book.CopyToAsync(stream);
            }

            var NewCourse = new Course();
            NewCourse.CollegeId = course.CollegeId;
            NewCourse.Book = FileName;
            NewCourse.Code = course.Code;
            NewCourse.Name = course.Name;
            NewCourse.Description = course.Description;
            NewCourse.LevelNumber = course.LevelNumber;
            _context.Courses.Add(NewCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "College",
                new { CollegeId = course.CollegeId, LevelId = course.LevelNumber });
        }

        var college = _context.Colleges.FirstOrDefault(c => c.ID == course.CollegeId);
        ModelState.AddModelError("Book", "Please upload a book");
        TempData["CollegeId"] = course.CollegeId;
        TempData["Level"] = LevelId;
        TempData["Levels"] = college.NumberOfLevels;
        TempData["College"] = college.Name;
        return View(course);
    }

    // GET: Course/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var course = _context.Courses.Include(c => c.College).FirstOrDefault(c => c.ID == id);
        TempData["CollegeId"] = course.CollegeId;
        TempData["Level"] = course.LevelNumber;
        TempData["Levels"] = course.College.NumberOfLevels;
        TempData["College"] = course.College.Name;
        TempData["Id"] = id;
        var courseVM = new CourseViewModel();
        courseVM.CollegeId = course.CollegeId;
        courseVM.Code = course.Code;
        courseVM.Name = course.Name;
        courseVM.Description = course.Description;
        courseVM.LevelNumber = course.LevelNumber;
        return View(courseVM);
    }

    // POST: Course/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseViewModel course)
    {
        if (ModelState.IsValid)
        {
            var NewCourse = _context.Courses.FirstOrDefault(c => c.ID == id);
            NewCourse.CollegeId = course.CollegeId;
            NewCourse.Code = course.Code;
            NewCourse.Name = course.Name;
            NewCourse.Description = course.Description;
            NewCourse.LevelNumber = course.LevelNumber;
            if (course.Book is not null && course.Book.Length > 0)
            {
                // Delete the existing file
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Books", NewCourse.Book);
                if (System.IO.File.Exists(existingFilePath)) System.IO.File.Delete(existingFilePath);

                var FileName = Guid.NewGuid().ToString() + '_' + course.Book.FileName;
                var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Books", FileName);

                using (var stream = new FileStream(FilePath, FileMode.Create))
                {
                    await course.Book.CopyToAsync(stream);
                }

                NewCourse.Book = FileName;
            }

            _context.Update(NewCourse);
            await _context.SaveChangesAsync();
            var college = _context.Colleges.FirstOrDefault(c => c.ID == course.CollegeId);
            TempData["CollegeId"] = course.CollegeId;
            TempData["Level"] = course.LevelNumber;
            TempData["Levels"] = college.NumberOfLevels;
            TempData["College"] = college.Name;
            return RedirectToAction("Details", "College",
                new { CollegeId = course.CollegeId, LevelId = course.LevelNumber });
        }

        var c = await _context.Courses.FindAsync(id);
        TempData["CollegeId"] = c.CollegeId;
        TempData["Level"] = c.LevelNumber;
        TempData["Levels"] = c.College.NumberOfLevels;
        TempData["College"] = c.College.Name;
        TempData["Id"] = id;
        return View(course);
    }

    // GET: Course/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Courses == null) return NotFound();

        var course = await _context.Courses
            .Include(c => c.College)
            .FirstOrDefaultAsync(m => m.ID == id);

        TempData["CollegeId"] = course.CollegeId;
        TempData["Level"] = course.LevelNumber;
        TempData["Levels"] = course.College.NumberOfLevels;
        TempData["College"] = course.College.Name;
        return View(course);
    }


    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _context.Enrollments.RemoveRange(_context.Enrollments.Where(e => e.CourseId == id).ToList());
        var course = await _context.Courses.FindAsync(id);
        var FilePhath = Path.Combine(_webHostEnvironment.WebRootPath, "Books", course.Book);
        if (System.IO.File.Exists(FilePhath)) System.IO.File.Delete(FilePhath);
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "College",
            new { CollegeId = course.CollegeId, LevelId = course.LevelNumber });
    }
    // POST: Course/Delete/5
}