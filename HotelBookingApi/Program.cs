using HotelBookingApi.Data;
using HotelBookingApi.Interfaces;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<EmailService>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();


// Comprobación de la conexión a la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (!context.Database.CanConnect())
    {
        Console.WriteLine("No se puede conectar a la base de datos.");
    }
    else
    {
        Console.WriteLine("conexion ok");
    }
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.Use(async (context, next) =>
{
    if(context.Request.Path == "/")
    {
        context.Response.Redirect("suaguer/index.html");
    }
    await next();
});

app.MapControllers();
app.Run();

