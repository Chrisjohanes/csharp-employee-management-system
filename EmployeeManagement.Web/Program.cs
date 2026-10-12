using CSharpDotNetLearning.Repositories;
using CSharpDotNetLearning.Services;

var builder = WebApplication.CreateBuilder(args);

var dataFilePath = Path.GetFullPath(
    Path.Combine(
        builder.Environment.ContentRootPath,
        "..",
        "CSharpDotNetLearning",
        "Data",
        "employees.json"
    )
);

builder.Services.AddSingleton<IEmployeeRepository>(
    _ => new EmployeeRepository(dataFilePath)
);

builder.Services.AddSingleton<EmployeeService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
