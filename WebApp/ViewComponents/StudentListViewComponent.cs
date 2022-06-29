using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<List<Student>> GetItemsAsync()
        {
            return _context.Students.ToListAsync();
        }
    }
}
