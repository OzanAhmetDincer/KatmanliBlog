using KatmanliBlogSitesi.Data;
using KatmanliBlogSitesi.Data.Abstract;
using KatmanliBlogSitesi.Data.Concrete;
using KatmanliBlogSitesi.Service.Abstract;
using KatmanliBlogSitesi.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(); // veritabaný tabolarýmýzý temsil eden entity framework classý."DatabaseContext" projemize tanýttýk. Bu özel bir class bunu tanýtmamýz lazým

// Authentication: oturum açma servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // admin controller da authorize eklediðimizde giriþ yapmayan kullanýcýlarý admin/login sayfasýna yönlendirir
    x.AccessDeniedPath = "/AccessDenied";// giriþ yapan kullanýcýnýn admin yetkisi yoksa AccessDenied sayfasýna yönlendir
    x.LogoutPath = "/Admin/Login/Logout";// çýkýþ sayfasý 
    x.Cookie.Name = "AdminLogin";
    x.Cookie.MaxAge = TimeSpan.FromDays(1);// oluþacak kukinin yaþam süresi
});// admin login iþlemi eklediðimiz zaman bu kod login iþlemi için programý hazýrlýyor, servisleri ayaða kaldýrýyor.

// Authorization : Yetkilendirme
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

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

app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
      );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
