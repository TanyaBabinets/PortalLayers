using Microsoft.EntityFrameworkCore;
using PortalLayers.BLL.Interfaces;
using PortalLayers.BLL.Services;
using PortalLayers.BLL.Infrastructure;
using PortalLayers.BLL.DTO.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSongContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddTransient<IGenreService, GenreService>();

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();
app.UseSession();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

















