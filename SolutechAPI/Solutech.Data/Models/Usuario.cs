using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Solutech.Data.Models;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    [Column("usuarioID")]
    public int UsuarioId { get; set; }

    [Column("nombreUsuario")]
    [StringLength(50)]
    [Unicode(false)]
    public string NombreUsuario { get; set; } = null!;

    [Column("passwordUsuario")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PasswordUsuario { get; set; }

    [Column("ultimoAcceso", TypeName = "datetime")]
    public DateTime UltimoAcceso { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("fechaCreacion", TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Column("fechaModificacion", TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
