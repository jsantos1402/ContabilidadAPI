using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContabilidadAPI.Models;

public partial class fiCuentasNiveles
{
    public int NivelId { get; set; }

    public string? NivelNombre { get; set; }

    public bool? Transaccional { get; set; }

    [JsonIgnore]
    public virtual ICollection<fiCuentas> FiCuenta { get; } = new List<fiCuentas>();
}
