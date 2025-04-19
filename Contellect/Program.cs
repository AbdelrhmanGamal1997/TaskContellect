using BusinessLogicProject.ServicesBL.ContactBLL;
using BusinessLogicProject.ServicesBL.UserBLL;
using Contellect.SignlR;
using CoreEntities;
using Microsoft.EntityFrameworkCore;
using Repository.InterFace;
using Repository.RepositryPattern;
using Repository.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

#region SqlConnections
builder.Services.AddDbContext<AppDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
#region Register
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContactBL, ContactBL>();

#endregion

builder.Services.AddSignalR();

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
app.MapHub<ContactHub>("/ContactHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=ContactGetAll}/{id?}");

app.Run();
