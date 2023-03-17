using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContabilidadAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.Numerics;

namespace ContabilidadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fiDiariosController : ControllerBase
    {
        public readonly ContabilidadDbContext dbContext;
        public fiDiariosController(ContabilidadDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Lista()
        {
            List<fiDiarios> Lista = new List<fiDiarios>();

            try
            {
                Lista = dbContext.FiDiarios.Include(d => d.Detalles).ToList();
                
                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Success", Respuesta = Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message, Respuesta = Lista });
            }
        }

        [HttpGet]
        [Route("ObtenerByID/{CompaniaID}/{OficinaID}/{TransaccionID}")]
        public IActionResult ObtenerByID(string CompaniaID, string OficinaID, int TransaccionID)
        {
            try
            {
                fiDiarios fiDiarios = dbContext.FiDiarios.Where(H => H.CompaniaId == CompaniaID && H.OficinaId == OficinaID && H.TransaccionId == TransaccionID).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Success", Respuesta = fiDiarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message, Respuesta = "" });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] fiDiariosView model)
        {
            int TransaccionDetalleID = 1;
            
            try
            {
                var Header = new fiDiarios
                {
                    CompaniaId = model.Encabezado.CompaniaId,
                    OficinaId = model.Encabezado.OficinaId,
                    TransaccionId = model.Encabezado.TransaccionId,
                    Numero = model.Encabezado.Numero,
                    Fecha = model.Encabezado.Fecha,
                    Estatus = model.Encabezado.Estatus
                };

                dbContext.FiDiarios.Add(Header);
                dbContext.SaveChanges();

                foreach (var detalle in model.Detalle)
                {
                    var detalleDb = new FiDiariosDetalle
                    {
                        CompaniaId = detalle.CompaniaId,
                        OficinaId = detalle.OficinaId,
                        TransaccionId = detalle.TransaccionId,
                        TransaccionDetalleId = TransaccionDetalleID,
                        CuentaId = detalle.CuentaId,
                        Debito = detalle.Debito,
                        Credito = detalle.Credito
                    };

                    dbContext.FiDiariosDetalles.Add(detalleDb);
                    dbContext.SaveChanges();
                    TransaccionDetalleID++;
                }

                return StatusCode(StatusCodes.Status201Created, new { Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message });
            }
        }
    }
}
