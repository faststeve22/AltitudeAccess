using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.DataAccessLayer.DAOs;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using AltitudeAccess.ServiceLayer.Services;
using Microsoft.AspNetCore.Identity;
using AltitudeAccess.DataAccessLayer.Factory;
using AltitudeAccess.Middleware;
using AltitudeAccess.ServiceLayer.Models;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDb");

// Add services to the container.

builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<IRoleDAO, RoleDAO>();
builder.Services.AddTransient<IUserDAO, UserDAO>();
builder.Services.AddTransient<IApplicationDAO, ApplicationDAO>();
builder.Services.AddTransient<IPasswordDAO, PasswordDAO>();
builder.Services.AddTransient<IUserApplicationRoleDAO, UserApplicationRoleDAO>();
builder.Services.AddTransient<IDbConnectionFactory>(sp => new SqlConnectionFactory(connectionString));
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<IPasswordManagementService, PasswordManagementService>();
builder.Services.AddTransient<IValidationService, ValidationService>();
builder.Services.AddSingleton<ITokenService>(sp => new TokenService(builder.Configuration["JWT:Secret"], int.Parse(builder.Configuration["JWT:Expiration"]), sp.GetRequiredService<IUserApplicationRoleDAO>()));
builder.Services.AddTransient<IMessageService, MessageService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddConsole();
    config.AddDebug();
    config.AddEventSourceLogger();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<CustomExceptionHandler>();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
