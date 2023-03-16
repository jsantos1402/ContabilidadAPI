using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContabilidadAPI.Models;

public partial class fiCuentas
{
    public string CuentaId { get; set; } = null!;

    public string? CuentaNombre { get; set; }

    public string? Origen { get; set; }

    public string? Estatus { get; set; }

    public string? CuentaControlId { get; set; }

    public int? NivelId { get; set; }

    [JsonIgnore]
    public virtual ICollection<FiDiariosDetalle> FiDiariosDetalles { get; } = new List<FiDiariosDetalle>();

    public virtual fiCuentasNiveles? _fiNiveles { get; set; }
}
