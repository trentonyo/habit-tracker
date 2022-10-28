using Habit_Tracker___Doveloop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HabitTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Habit_Tracker___DoveloopContext") ?? throw new InvalidOperationException("Connection string 'Habit_Tracker___DoveloopContext' not found.")));

//cosmos connection
var cosmosInfo = builder.Configuration.GetSection("CosmosDB");
builder.Services.AddSingleton<ICosmosDbService>(new CosmosDbService(new CosmosClient(cosmosInfo["connectionString"]), cosmosInfo["DBName"], cosmosInfo["HabitContainer"]));

// Add services to the container.
builder.Services.AddDbContext<CosmosDbContext>(options =>
    options.UseCosmos(cosmosInfo["connectionString"], cosmosInfo["DBName"]));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CosmosDbContext>();

builder.Services.AddIdentityServer().AddApiAuthorization<IdentityUser, CosmosDbContext>();

var configuration = builder.Configuration;
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
