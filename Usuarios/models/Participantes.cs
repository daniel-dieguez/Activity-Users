using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Usuarios.models;


[Table("Usuario")] 
public class Participantes
{
    [Key] 
    [Column("id_usuario")] 
    public int id_usuario{ get; set; }

    [Column("nombre_usuario")]
    public string nombre_usuario { get; set; } = string.Empty;

    [Column("correo_usaurio")] 
    public string correo_usaurio{ get; set; }  = string.Empty;

    [Column("id_actividad")] 
    public int id_actividad;

    
    public Actividad Actividad { get; set; } = null!;

}