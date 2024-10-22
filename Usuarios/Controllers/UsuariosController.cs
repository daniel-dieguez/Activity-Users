using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuarios.Contexts;
using Usuarios.models;

namespace Usuarios.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : Controller
{

    private readonly ActividadesContext _actividadesContext;
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(ActividadesContext actividadesContext, ILogger<UsuariosController> logger)
    {
        _actividadesContext = actividadesContext;
        _logger = logger;
    } //generet contructor

    [HttpGet("/participantes/viewAll")]
    public async Task<ActionResult<IEnumerable<Participantes>>> viewAll()
    {
        _logger.LogInformation("Search all users");
        var participante = await _actividadesContext.Participantes
            .Include(p => p.Actividad)
            .ToListAsync();
        return Ok(participante);
    }

    [HttpPost("/participantes/new")]
    public async Task<IActionResult> postParticipante([FromBody]Participantes participantes)
    {
        try
        {
            var actividad = await _actividadesContext.Actividades.FindAsync(participantes.id_actividad);
            if (actividad == null)
            {
                return NotFound("not found actividity, try again witch diferent number");

            }

            //Assing id activity
            participantes.Actividad = null;
            
            await _actividadesContext.Participantes.AddAsync(participantes);
            await _actividadesContext.SaveChangesAsync();
            _logger.LogInformation("Great! add a new user");
            return CreatedAtAction(nameof(viewAll), new { id = participantes.id_usuario }, participantes);

        }
        catch (Exception e)
        {
            _logger.LogInformation($"Sorry, the request isn´t found ${e.Message}");
            return StatusCode(500, $"fault to add a new user ${e.Message}");
        }
    }
    
    
    



}