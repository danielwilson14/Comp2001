// Main entry point for the application.
using Comp2001.Data;
using Microsoft.EntityFrameworkCore;
using Comp2001.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Service configuration.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registering AuthenticationService for dependency injection.
builder.Services.AddHttpClient<AuthenticationService>();

// Building and running the application.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
