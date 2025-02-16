using SchedulerEngine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SchedulerHandler>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var schedulerHandler = app.Services.GetRequiredService<SchedulerHandler>();
schedulerHandler.StartAll();

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
    pattern: "{controller=Job}/{action=Dashboard}/{id?}");

app.Run();
