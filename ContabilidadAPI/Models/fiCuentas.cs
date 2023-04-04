﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
    public virtual ICollection<fiDiariosDetalle> FiDiariosDetalles { get; } = new List<fiDiariosDetalle>();

    public virtual fiCuentasNiveles? _fiNiveles { get; set; }
}
