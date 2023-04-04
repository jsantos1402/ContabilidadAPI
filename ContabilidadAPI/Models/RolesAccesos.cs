using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models;

public partial class RolesAccesos
{
    public string RolId { get; set; } = null!;

    public bool? Consultar { get; set; }

    public bool? Insertar { get; set; }

    public bool? Modificar { get; set; }

    public bool? Eliminar { get; set; }

    public virtual Roles Rol { get; set; } = null!;
}
