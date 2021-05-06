using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mock6.Data;
using Mock6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mock6.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public HomeController(EmployeeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Employee()
        {
            return View(await _context.Employee.ToListAsync());
        }

        public async Task<IActionResult> RetirementInfo(int Id)
        {
            //var guid = Guid.NewGuid();
            var model = new RetirementViewModel();
            var employee = await _context.Employee.FindAsync(Id);

            if (employee.Age > 60)
            {
                //ViewBag.CanRetire = true;
                model.CanRetire = true;
            }
            else
            {
                //ViewBag.CanRetire = false;
                model.CanRetire = false;

            }

            //ViewBag.Benefits = employee.Salary * 0.6M;
            model.Benefits = employee.Salary * 0.6M;

            return View(model);
        }

        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Employee));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
