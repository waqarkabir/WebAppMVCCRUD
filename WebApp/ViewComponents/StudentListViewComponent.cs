using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using System.Linq;
using WebApp.Models;

namespace WebApp.ViewComponents
{
    public class StudentListViewComponent : ViewComponent
    {
        private readonly StudentDbContext _context;

        public StudentListViewComponent(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name, string cnic)
        {
            var items = await GetItemsAsync(name, cnic);
            return View(items);
        }

        private async Task<List<Student>> GetItemsAsync(string name, string cnic)
        {
            var students = await _context.Students.ToListAsync();


            if (!string.IsNullOrEmpty(name) || !string.IsNullOrWhiteSpace(name))
            {
                students = students.Where(p => p.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(cnic) || !string.IsNullOrWhiteSpace(cnic))
            {
                students = students.Where(p => p.Cnic.ToUpper().Contains(cnic.ToUpper())).ToList();
            }

            return students;
        }
    }
}
