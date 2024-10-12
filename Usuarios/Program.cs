using Microsoft.EntityFrameworkCore;
using Usuarios.Contexts;

var builder = WebApplication.CreateBuilder(args);


// Configurar el DbContext para ActividadesContext
builder.Services.AddDbContext<ActividadesContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Agregar servicios de autorización
builder.Services.AddAuthorization();

var app = builder.Build();

// Configurar el pipeline de la aplicación HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseRouting();

app.UseAuthentication(); // Si estás usando autenticación, asegúrate de tener esto
app.UseAuthorization();  // Aquí agregas el middleware de autorización

app.MapControllers();

app.Run();
