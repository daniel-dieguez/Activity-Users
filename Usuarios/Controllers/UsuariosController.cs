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
    } //genere por medio de contructor

    [HttpGet("/participantes/viewAll")]
    public async Task<ActionResult<IEnumerable<Participantes>>> viewAll()
    {
        _logger.LogInformation("Se ejecuta consulta en dato de usuarios");
        var participante = await _actividadesContext.Participantes
                .Include(p => p.Actividad)
                .ToListAsync();
        return Ok(participante);
    }
    
    
}