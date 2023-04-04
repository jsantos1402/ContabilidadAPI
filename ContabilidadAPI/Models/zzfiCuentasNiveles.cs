using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContabilidadAPI.Models;

public partial class zzfiCuentasNiveles
{
    public int NivelId { get; set; }

    public string? NivelNombre { get; set; }

    public bool? Transaccional { get; set; }

    [JsonIgnore]
    public virtual ICollection<zzfiCuentas> FiCuenta { get; } = new List<zzfiCuentas>();
}
