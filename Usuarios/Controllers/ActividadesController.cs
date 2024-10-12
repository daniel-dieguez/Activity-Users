using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuarios.Contexts;
using Usuarios.models;

namespace Usuarios.Controllers;


[ApiController]
[Route("[controller]")]
public class ActividadesController : ControllerBase
{
    private readonly ActividadesContext _actividadesContext;
    private readonly ILogger<ActividadesController> _logger;

    public ActividadesController(ActividadesContext actividadesContext, ILogger<ActividadesController> logger)
    {
        _actividadesContext = actividadesContext;
        _logger = logger;
    }

    [HttpGet("/viewAll")] // Aquí se ajusta la ruta
    public async Task<ActionResult<IEnumerable<Actividad>>> GetActividades() // Cambiado a IEnumerable
    {
        _logger.LogInformation("Se ejecutó consulta de vista");
        
        var actividades = await _actividadesContext.Actividades.ToListAsync();

        if (actividades == null || !actividades.Any())
        {
            return NotFound(); // Manejo de error si no hay actividades
        }

        return Ok(actividades); // Retorna un 200 con la lista de actividades
    }

    [HttpPost("/newActividad")]
    public async Task<IActionResult> postActividad( Actividad actividad)
    {
        try
        {
            await _actividadesContext.Actividades.AddAsync(actividad);
            await _actividadesContext.SaveChangesAsync();

            _logger.LogInformation("Se realizó una nueva actividad");
            return CreatedAtAction(nameof(GetActividades), new { id = actividad.id_activad }, actividad);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar nueva actividad");
            return StatusCode(500, "Error al agregar nueva actividad");
        }
    }

    [HttpPut("/update/{id_activad}")]
    public async Task<ActionResult<Dictionary<String, Object>>> updateActividad(int id_activad, Actividad actividad)
    {
        var response = new Dictionary<String, Object>();

        
        var existingActividad = await _actividadesContext.Actividades.FindAsync(id_activad);
        existingActividad.actividad = actividad.actividad; // Solo actualiza los campos necesarios

        // Guarda los cambios
        await _actividadesContext.SaveChangesAsync();

        response.Add("mensaje", "Se actualizó la actividad");
        return Ok(response);
    }

    [HttpDelete("/eliminar/{id_activad}")]
    public async Task<ActionResult<Dictionary<string, object>>> delete(int id_activad)
    {
        var response = new Dictionary<string, object>();
    
        // Busca la actividad por ID
        var activity = await _actividadesContext.Actividades.FindAsync(id_activad);

        // Verifica si la actividad existe
        if (activity == null)
        {
            return NotFound(new { mensaje = "Actividad no encontrada" });
        }

        // Elimina la actividad
        _actividadesContext.Actividades.Remove(activity);
    
        try
        {
            await _actividadesContext.SaveChangesAsync();
            _logger.LogInformation($"La actividad con ID {id_activad} eliminada exitosamente.");
            response.Add("mensaje", "Se eliminó la actividad");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar la actividad con ID {id_activad}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la actividad.");
        }
    }

}
