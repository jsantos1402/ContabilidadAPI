using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContabilidadAPI.Models;

public partial class zzfiCuentas
{
    public string CuentaId { get; set; } = null!;

    public string? CuentaNombre { get; set; }

    public string? Origen { get; set; }

    public string? Estatus { get; set; }

    public string? CuentaControlId { get; set; }

    public int? NivelId { get; set; }

    [JsonIgnore]
    public virtual ICollection<zzfiDiariosDetalle> FiDiariosDetalles { get; } = new List<zzfiDiariosDetalle>();

    public virtual zzfiCuentasNiveles? _fiNiveles { get; set; }
}
