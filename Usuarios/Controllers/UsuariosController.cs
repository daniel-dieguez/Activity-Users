using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http.HttpResults;
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
        var response = new Dictionary<String, Object>();
        _logger.LogInformation("Search all users");
        response.Add("Message", "consulting all users");
        var participante = await _actividadesContext.Participantes
            .Include(p => p.Actividad)
            .ToListAsync();
        return Ok(participante);
    }

    [HttpPost("/participantes/new")]
    public async Task<IActionResult> postParticipante([FromBody]Participantes participantes)
    {
        var response = new Dictionary<String, Object>();
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
            response.Add("Message", "creating new user");
            _logger.LogInformation("Great! add a new user");
            return CreatedAtAction(nameof(viewAll), new { id = participantes.id_usuario }, participantes);

        }
        catch (Exception e)
        {
            _logger.LogInformation($"Sorry, the request isn´t found ${e.Message}");
            return StatusCode(500, $"fault to add a new user ${e.Message}");
        }
    }

    [HttpPut("/participantes/update/{id_usuario}")]
    public async Task<ActionResult<Dictionary<String, Object>>> update(int id_usuario, Participantes participantes)
    {
        var response = new Dictionary<String, Object>();
        try
        {
            var usuarioExist = await _actividadesContext.Participantes.FindAsync(id_usuario);
            if (usuarioExist == null)
            {
                return NotFound("Not found id_usuario, try again");
            }
            
            var actividad = await _actividadesContext.Actividades.FindAsync(participantes.id_actividad);
            if (actividad == null)
            {
                return NotFound("not found actividity, try again witch diferent number");

            }
            
            //data to update
            usuarioExist.nombre_usuario = participantes.nombre_usuario;
            usuarioExist.correo_usaurio = participantes.correo_usaurio;
            actividad.id_activad = actividad.id_activad;
            
            
            await _actividadesContext.SaveChangesAsync();
            _logger.LogInformation("Update is exit");
            response.Add("Message", "Update  is exit");
            return Ok(response);
        }catch(Exception e){
        _logger.LogInformation($"Error to update user ${e.Message}");
        return StatusCode(500, $"Fault to update user ${e.Message}");
        }
    }
    
    
    
    



}