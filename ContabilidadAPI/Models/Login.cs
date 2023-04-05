using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;


namespace ContabilidadAPI.Models
{
    public class Login
    {
        private readonly ContabilidadDbContext _dbContext;

        public Login(ContabilidadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        Procesado = false,
                        Respuesta = "",
                        Mensaje = "Token Invalido"
                    };
                }

                var UsuarioID = identity.Claims.FirstOrDefault(x => x.Type == "UsuarioID").Value;
                var Correo = identity.Claims.FirstOrDefault(x => x.Type == "Correo").Value;
                var Contrasena = identity.Claims.FirstOrDefault(x => x.Type == "Contrasena").Value;

                Usuarios usuario = _dbContext.Usuarios.Where(u => u.UsuarioId == UsuarioID && u.Correo == Correo && u.Contrasena == Contrasena).FirstOrDefault();

                if (usuario == null) 
                {
                    return new
                    {
                        Procesado = false,
                        Respuesta = "",
                        Mensaje = "Token Invalido"
                    };
                }
                return new { Procesado = true, Respuesta = "Success", Mensaje = usuario };
            }
            catch (Exception ex)
            {
                return new { Procesado = false, Respuesta = "Failed", Mensaje = ex.Message };
            }
        }

    }
}
