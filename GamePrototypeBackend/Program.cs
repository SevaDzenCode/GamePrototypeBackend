using GamePrototypeBackend;
using GamePrototypeBackend.BL;
using GamePrototypeBackend.BL.Mapping;
using GamePrototypeBackend.BL.Services;
using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.EF;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository;
using GamePrototypeBackend.Data.Repository.Interfaces;
using GamePrototypeBackend.Data.Repository.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddDbContext<GamePrototypeDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GamePrototype;Trusted_Connection=True"));
builder.Services.AddCoreAdmin();
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<GamePrototypeDbContext>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMessageSender, MessageSender>();
builder.Services.AddTransient<IAdminPanelRepository, AdminPanelRepository>();
builder.Services.AddTransient<IAdminPanelService, AdminPanelService>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();
