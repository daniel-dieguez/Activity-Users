using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Usuarios.models;

[Table("actividades")]
public class Actividad
{
    
    [Key]
    [Column("id_activad")] // Si en la base de datos se llama 'ID'
    public int id_activad { get; set; }
    [Column("actividad")]
    public string actividad { get; set; } = String.Empty;

    [JsonIgnore]
    public List<Participantes> Participantes { get; set; } = null!;
}