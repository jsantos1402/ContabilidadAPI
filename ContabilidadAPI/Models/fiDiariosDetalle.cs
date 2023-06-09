﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models;

public partial class fiDiariosDetalle
{
    public string CompaniaId { get; set; } = null!;

    public string OficinaId { get; set; } = null!;

    public long TransaccionId { get; set; }

    public long TransaccionDetalleId { get; set; }

    public string? CuentaId { get; set; }

    public decimal? Debito { get; set; }

    public decimal? Credito { get; set; }

    [JsonIgnore]
    public virtual fiCuentas? Cuenta { get; set; }
}
