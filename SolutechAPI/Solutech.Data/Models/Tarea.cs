using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Solutech.Data.Models;

public partial class Tarea
{
    [Key]
    [Column("tareaID")]
    public int TareaId { get; set; }

    [Column("usuarioID")]
    public int UsuarioId { get; set; }

    [Column("titulo")]
    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("fechaCreacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("fechaVencimiento", TypeName = "datetime")]
    public DateTime FechaVencimiento { get; set; }

    [Column("fechaModificacion", TypeName = "datetime")]
    public DateTime FechaModificacion { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("Tareas")]
    public virtual Usuario Usuario { get; set; } = null!;
}
