using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models;

public partial class FiDiariosDetalle
{
    public string CompaniaId { get; set; } = null!;

    public string OficinaId { get; set; } = null!;

    public string TransaccionId { get; set; } = null!;

    public long TransaccionDetalleId { get; set; }

    public string? CuentaId { get; set; }

    public decimal? Debito { get; set; }

    public decimal? Credito { get; set; }

    public virtual fiCuentas? Cuenta { get; set; }
}
