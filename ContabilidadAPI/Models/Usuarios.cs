using System;
using System.Collections.Generic;

namespace ContabilidadAPI.Models;

public partial class Usuarios
{
    public string UsuarioId { get; set; } = null!;

    public string? UsuarioNombre { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public string? RolId { get; set; }

    public virtual Roles? Rol { get; set; }
}
