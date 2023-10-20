using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Materias.Entities;

[Table("materias")]
public class Materia
{
    [Column("id")]
    public int Id { get; set; }
    [Column("codigo")]
    [Required]
    public string Codigo { get; set; }
    [Column("nombre")]
    [Required]
    public string Nombre { get; set; }
    [Column("requisito_id")]
    public int RequisitoId { get; set; }
    [ForeignKey(nameof(RequisitoId))]
    public virtual Materia Requisito { get; set; }
}