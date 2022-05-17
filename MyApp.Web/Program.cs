using MyApp.Modules.Products;
using MyApp.Modules.Shared;
using MyApp.Web.Extensions;
using MyApp.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseCors(options => options
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader());
}

app.UseStaticFiles();

app.UseStaticFiles();
app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.MapFallbackToFile("index.html");
}

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.InitProducts(app.Environment);
    scope.ServiceProvider.InitSubscriptions(app.Environment);
}

app.Run();
