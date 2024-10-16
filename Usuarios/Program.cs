using Microsoft.EntityFrameworkCore;
using Usuarios.Contexts;

var builder = WebApplication.CreateBuilder(args);



// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Configurar el DbContext para ActividadesContext
builder.Services.AddDbContext<ActividadesContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




// Agregar servicios de autorización
builder.Services.AddAuthorization();

// Swagger/OpenAPI configuración para documentar tu API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de la aplicación HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // Activa Swagger para la documentación
    app.UseSwaggerUI();    // UI de Swagger para visualizar y probar la API
}


app.UseRouting();

app.UseAuthentication(); // Si estás usando autenticación, asegúrate de tener esto
app.UseAuthorization();  // Aquí agregas el middleware de autorizaciónx

app.MapControllers();

app.Run();
