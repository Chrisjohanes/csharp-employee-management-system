using CSharpDotNetLearning.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Controllers;

public class EmployeesController : Controller
{
    private readonly EmployeeService _employeeService;

    public EmployeesController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: /Employees
    public IActionResult Index()
    {
        return View(_employeeService.Employees);
    }

    // GET: /Employees/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Employees/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        string name,
        string position,
        int age,
        decimal salary)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(position) ||
            age <= 0 ||
            age > 120 ||
            salary < 0)
        {
            ViewBag.ErrorMessage =
                "Please enter valid employee information.";

            return View();
        }

        _employeeService.AddEmployee(
            name.Trim(),
            position.Trim(),
            age,
            salary);

        TempData["SuccessMessage"] =
            "Employee added successfully.";

        return RedirectToAction(nameof(Index));
    }

    // POST: /Employees/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
        int id,
        string name,
        string position,
        int age,
        decimal salary)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(position) ||
            age <= 0 ||
            age > 120 ||
            salary < 0)
        {
            TempData["ErrorMessage"] =
                "Please enter valid employee information.";

            return RedirectToAction(nameof(Index));
        }

        bool updated = _employeeService.UpdateEmployee(
            id,
            name.Trim(),
            position.Trim(),
            age,
            salary);

        if (!updated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] =
            "Employee updated successfully.";

        return RedirectToAction(nameof(Index));
    }

 // POST: /Employees/Delete
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Delete(int id)
    {
        bool deleted = _employeeService.DeleteEmployee(id);

        if (!deleted)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] =
            "Employee deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}