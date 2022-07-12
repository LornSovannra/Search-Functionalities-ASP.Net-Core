using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Search_Funct.Data;
using Search_Funct.Models;
using System.Diagnostics;

namespace Search_Funct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        void CONFIG()
        {
            var gender = new List<SelectListItem>
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" },
            };

            ViewBag.gender = gender;

            var departments = _context.Department.ToList();

            ViewBag.department = departments;
        } 

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var data = _context.Employees.Include(x => x.Department).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Index(string search_param)
        {
            ViewData["EmployeeFiltered"] = search_param;

            var employee = from x in _context.Employees select x;

            if (!String.IsNullOrEmpty(search_param))
            {
                employee = employee.Where(x => x.Fname.Contains(search_param) || x.Lname.Contains(search_param) || x.Gender.Contains(search_param) || x.Email.Contains(search_param) || x.Address.Contains(search_param));
            }

            return View(employee.AsNoTracking().ToList());
        }

        public IActionResult Create()
        {
            CONFIG();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            CONFIG();

            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();

                TempData["SuccessMsg"] = "Employee created successfully.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            CONFIG();

            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

            if(employee == null || id == 0)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            CONFIG();

            if (ModelState.IsValid)
            {
                _context.Update(model);
                _context.SaveChanges();

                TempData["SuccessMsg"] = "Employee updated successfully.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                TempData["SuccessMsg"] = "Unable to delete Employee.";
                return RedirectToAction("Index");
            }

            var employee = _context.Employees.Find(id);
            
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            TempData["SuccessMsg"] = "Employee deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}