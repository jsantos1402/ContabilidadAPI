using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContabilidadAPI.Models;

public partial class zzfiDiariosDetalle
{
    public string CompaniaId { get; set; } = null!;

    public string OficinaId { get; set; } = null!;
    
    public long TransaccionId { get; set; }
    
    public long TransaccionDetalleId { get; set; }
    
    public string? CuentaId { get; set; }
    
    public decimal? Debito { get; set; }
    
    public decimal? Credito { get; set; }
    [JsonIgnore]
    public virtual zzfiCuentas? Cuenta { get; set; }

    //public fiDiarios fiDiarios { get; set; }

    //public int ClaveCompuesta => CompaniaId.GetHashCode() ^ OficinaId.GetHashCode() ^ TransaccionId.GetHashCode();
}
