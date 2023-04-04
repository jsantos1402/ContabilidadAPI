using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models;

public partial class Roles
{
    public string RolId { get; set; } = null!;

    public string? RolNombre { get; set; }

    public string? Estatus { get; set; }

    public virtual RolesAccesos? RolesAcceso { get; set; }

    public virtual ICollection<Usuarios> Usuarios { get; } = new List<Usuarios>();
}
