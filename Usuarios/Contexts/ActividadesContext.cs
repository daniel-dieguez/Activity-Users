using Microsoft.EntityFrameworkCore;
using Usuarios.models;

namespace Usuarios.Contexts;

public class ActividadesContext: DbContext
{
    public ActividadesContext(DbContextOptions<ActividadesContext> options) : base(options)
    {
    }

    public DbSet<Actividad> Actividades { get; set; }
    
    
}