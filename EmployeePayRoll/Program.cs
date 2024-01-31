using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

var builder = WebApplication.CreateBuilder(args);


//clearing all default logging provider ==>  
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();


var _logger=builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
builder.Services.AddSingleton(typeof(ILogger),_logger);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IEmployeeBusiness,EmployeeBusiness>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
