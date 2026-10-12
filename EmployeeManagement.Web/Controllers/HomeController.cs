using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Web.Models;
using CSharpDotNetLearning.Services;

namespace EmployeeManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly EmployeeService _employeeService;

    public HomeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        var employees = _employeeService.Employees;

        return View(employees);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}