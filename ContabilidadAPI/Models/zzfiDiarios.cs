using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models;

public partial class zzfiDiarios
{
    public string CompaniaId { get; set; } = null!;

    public string OficinaId { get; set; } = null!;

    public long TransaccionId { get; set; } 

    public string? Numero { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estatus { get; set; }

    public ICollection<zzfiDiariosDetalle> Detalles { get; set; } = new List<zzfiDiariosDetalle>();

}
