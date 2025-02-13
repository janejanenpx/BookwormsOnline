using BookwormsOnline_231660A.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));


//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 12;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();


// Add session management
//ADDED
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddDistributedMemoryCache(); //save session in memory

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Cookie
builder.Services.ConfigureApplicationCookie(Config =>
{
	Config.LoginPath = "/Login";
});

// Configure anti-forgery
builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

//Protect data
builder.Services.AddDataProtection();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseSession();


app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    if (context.User.Identity.IsAuthenticated)
//    {
//        var sessionStart = context.Session.GetString("SessionStart");
//        if (string.IsNullOrEmpty(sessionStart))
//        {
//            context.Response.Redirect("/Login");
//            return;
//        }

//        var sessionTime = DateTime.Now - DateTime.Parse(sessionStart);
//        if (sessionTime.TotalMinutes > 20)
//        {
//            context.Response.Redirect("/Login");
//            return;
//        }
//    }
//    await next();
//});

app.MapRazorPages();

app.Run();
