using Microsoft.EntityFrameworkCore;
using Usuarios.models;

namespace Usuarios.Contexts;

public class ActividadesContext: DbContext
{
    public ActividadesContext(DbContextOptions<ActividadesContext> options) : base(options)
    {
    }

    public DbSet<Actividad> Actividades { get; set; } = null!;
    public DbSet<Participantes> Participantes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participantes>()
            .HasOne(p => p.Actividad)        // Un Participante tiene una Actividad
            .WithMany(a => a.Participantes)  // Una Actividad tiene muchos Participantes
            .HasForeignKey(p => p.id_actividad);
    }
    
    
}