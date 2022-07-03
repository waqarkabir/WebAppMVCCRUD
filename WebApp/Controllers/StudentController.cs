using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[Authorize]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentDbContext _context;

        public StudentController(ILogger<StudentController> logger, StudentDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateViewModel model)
        {
                var students = _context.Students;

            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Cnic = model.Cnic,
                    Name = model.Name
                };
            students.Add(student);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var student = _context.Students.FirstOrDefault(p => p.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new Student()
            {
                Cnic = student.Cnic,
                Name = student.Name
            };

            return View(model);
        }
        public IActionResult Update(int id)
        {
            var student = _context.Students.FirstOrDefault(p => p.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentUpdateViewModel()
            {
                Cnic = student.Cnic,
                Name = student.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(StudentUpdateViewModel model)
        {
            var student = _context.Students.FirstOrDefault(p => p.Id == model.Id);
            if (student == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                student.Name = model.Name;
                student.Cnic = model.Cnic;
                _context.Students.Update(student);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            return RedirectToAction("Index");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);


            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { Areas = "", Controller = "Student" });
        }

        public IActionResult GetData(string name, string cnic)
        {
            return ViewComponent("WebApp.ViewComponents.StudentList", new {name, cnic}
            );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}