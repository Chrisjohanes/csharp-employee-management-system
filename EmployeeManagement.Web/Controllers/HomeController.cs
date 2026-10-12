
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

    public IActionResult Index(
        string? searchTerm,
        string? position,
        string? sortBy)
    {
        // Get all employees from the service.
        var allEmployees = _employeeService.Employees;

        // Get unique positions for the filter dropdown.
        var positions = allEmployees
            .Select(employee => employee.Position)
            .Where(position => !string.IsNullOrWhiteSpace(position))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(position => position)
            .ToList();

        // Start with the complete employee collection.
        var employees = allEmployees.AsEnumerable();

        // Search employees by name or ID.
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            employees = employees.Where(employee =>
                employee.Name.Contains(
                    searchTerm,
                    StringComparison.OrdinalIgnoreCase
                )
                || employee.Id.ToString().Contains(searchTerm)
            );
        }

        // Filter employees by exact position.
        if (!string.IsNullOrWhiteSpace(position))
        {
            employees = employees.Where(employee =>
                employee.Position.Equals(
                    position,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        // Sort the filtered employee collection.
        employees = sortBy?.ToLowerInvariant() switch
        {
            "name" => employees.OrderBy(employee => employee.Name),
            "id" => employees.OrderBy(employee => employee.Id),
            "salary" => employees.OrderByDescending(
                employee => employee.Salary
            ),
            _ => employees
        };

        // Pass filter values and options to the view.
        ViewData["SearchTerm"] = searchTerm;
        ViewData["SelectedPosition"] = position;
        ViewData["Positions"] = positions;
        ViewData["SortBy"] = sortBy;

        return View(employees.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true
    )]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
        });
    }
}
