using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContabilidadAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ContabilidadAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class fiCuentasController : ControllerBase
    {
        public readonly ContabilidadDbContext dbContext;
        public fiCuentasController(ContabilidadDbContext _dbContext) { 
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Listar")]
        [Authorize]
        public IActionResult Listar() 
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            Login login = new Login(dbContext);

            var _Token = login.validarToken(Identity);

            if (!_Token.Procesado)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { _Token });
            }

            Usuarios usuario = _Token.Mensaje;

            RolesAccesos Accesos = dbContext.RolesAccesos.Where(R => R.RolId == usuario.RolId).FirstOrDefault();

            if (Accesos == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Mensaje = "El usuario no tiene una configuracion de roles correcta" });
            }

            if (Accesos.Consultar == true)
            {
                List<fiCuentas> Lista = new List<fiCuentas>();

                try
                {
                    Lista = dbContext.FiCuentas.Include(N => N._fiNiveles).ToList();

                    return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Success", Respuesta = Lista });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message, Respuesta = Lista });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Mensaje = "El usuario no tiene acceso a consultar" });
            }
        }

        [HttpGet]
        [Route("ObtenerByID/{CuentaID}")]
        [Authorize]
        public IActionResult ObtenerByID(string CuentaID) 
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            Login login = new Login(dbContext);

            var _Token = login.validarToken(Identity);

            if (!_Token.Procesado)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { _Token });
            }

            Usuarios usuario = _Token.Mensaje;

            RolesAccesos Accesos = dbContext.RolesAccesos.Where(R => R.RolId == usuario.RolId).FirstOrDefault();

            if (Accesos == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Mensaje = "El usuario no tiene una configuracion de roles correcta" });
            }

            if (Accesos.Consultar == true)
            {
                fiCuentas cuentas = dbContext.FiCuentas.Find(CuentaID);

                if (cuentas == null)
                {
                    return BadRequest("Cuenta no encontrada");
                }

                try
                {
                    cuentas = dbContext.FiCuentas.Include(N => N._fiNiveles).Where(f => f.CuentaId == CuentaID).FirstOrDefault();
                    return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Success", Respuesta = cuentas });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message, Respuesta = cuentas });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Mensaje = "El usuario no tiene acceso a consultar" });
            }
            
        }

        [HttpPost]
        [Route("Guardar")]
        [Authorize]
        public IActionResult Guardar([FromBody] fiCuentas cuentas)
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            Login login = new Login(dbContext);

            var _Token = login.validarToken(Identity);

            if (!_Token.Procesado)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { _Token });
            }

            Usuarios usuario = _Token.Mensaje;

            RolesAccesos Accesos = dbContext.RolesAccesos.Where(R => R.RolId == usuario.RolId).FirstOrDefault();

            if (Accesos == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Mensaje = "El usuario no tiene una configuracion de roles correcta" });
            }

            if (Accesos.Insertar == true)
            {
                try
                {
                    dbContext.FiCuentas.Add(cuentas);
                    dbContext.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Success" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message });
                }
            } else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Mensaje = "El usuario no tiene acceso a insertar" });
            }
            
        }

        [HttpPut]
        [Route("Modificar")]
        [Authorize]
        public IActionResult Modificar([FromBody] fiCuentas cuentas)
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            Login login = new Login(dbContext);

            var _Token = login.validarToken(Identity);

            if (!_Token.Procesado)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { _Token });
            }

            Usuarios usuario = _Token.Mensaje;

            RolesAccesos Accesos = dbContext.RolesAccesos.Where(R => R.RolId == usuario.RolId).FirstOrDefault();

            if (Accesos == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Mensaje = "El usuario no tiene una configuracion de roles correcta" });
            }

            if (Accesos.Modificar == true)
            {
                fiCuentas ofiCuentas = dbContext.FiCuentas.Find(cuentas.CuentaId);

                if (ofiCuentas == null)
                {
                    return BadRequest("Cuenta no encontrada");
                }

                try
                {
                    ofiCuentas.CuentaNombre = cuentas.CuentaNombre is null ? ofiCuentas.CuentaNombre : cuentas.CuentaNombre;
                    ofiCuentas.Origen = cuentas.Origen is null ? ofiCuentas.Origen : cuentas.Origen;
                    ofiCuentas.Estatus = cuentas.Estatus is null ? ofiCuentas.Estatus : cuentas.Estatus;
                    ofiCuentas.CuentaControlId = cuentas.CuentaControlId is null ? ofiCuentas.CuentaControlId : cuentas.CuentaControlId;
                    ofiCuentas.NivelId = cuentas.NivelId is null ? ofiCuentas.NivelId : cuentas.NivelId;

                    dbContext.FiCuentas.Update(ofiCuentas);
                    dbContext.SaveChanges();

                    return StatusCode(StatusCodes.Status202Accepted, new { Mensaje = "Success" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Mensaje = "El usuario no tiene acceso a modificar" });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{CuentaID}")]
        [Authorize]
        public IActionResult Eliminar(string CuentaID)
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;

            Login login = new Login(dbContext);
            var _Token = login.validarToken(Identity);
            if (!_Token.Procesado)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { _Token });
            }

            Usuarios usuario = _Token.Mensaje;

            RolesAccesos Accesos = dbContext.RolesAccesos.Where(R => R.RolId == usuario.RolId).FirstOrDefault();

            if (Accesos == null) 
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Mensaje = "El usuario no tiene una configuracion de roles correcta" });
            }

            if (Accesos.Eliminar == true)
            {
                fiCuentas cuentas = dbContext.FiCuentas.Find(CuentaID);

                if (cuentas is null)
                {
                    return BadRequest("Cuenta no encontrada");
                }

                try
                {
                    dbContext.FiCuentas.Remove(cuentas);
                    dbContext.SaveChanges();

                    return StatusCode(StatusCodes.Status202Accepted, new { Mensaje = "Success" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message });
                }
            } else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Mensaje = "El usuario no tiene acceso a eliminar" });
            }
        }
    }
}
