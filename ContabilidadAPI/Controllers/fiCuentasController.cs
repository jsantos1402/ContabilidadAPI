using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContabilidadAPI.Models;
using Microsoft.AspNetCore.Cors;

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
        public IActionResult Listar() { 
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

        [HttpGet]
        [Route("ObtenerByID/{CuentaID}")]
        public IActionResult ObtenerByID(string CuentaID) {

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

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] fiCuentas cuentas)
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
        }

        [HttpPut]
        [Route("Modificar")]
        public IActionResult Modificar([FromBody] fiCuentas cuentas)
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

        [HttpDelete]
        [Route("Eliminar/{CuentaID}")]

        public IActionResult Eliminar(string CuentaID)
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
        }
    }
}
