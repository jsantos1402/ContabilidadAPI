using ContabilidadAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContabilidadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IConfiguration _Configuration;
        public readonly ContabilidadDbContext dbContext;

        public UsuariosController(IConfiguration configuration, ContabilidadDbContext _dbContext)
        {
            _Configuration = configuration;
            dbContext = _dbContext;
        }

        [HttpPost]
        [Route("GetToken")]      
        public dynamic Login([FromBody] Object UsuarioData)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(UsuarioData.ToString());

                string User = data.UsuarioID.ToString();
                string Password = data.Password.ToString();

                Usuarios usuario = dbContext.Usuarios.Where(u => u.UsuarioId == User && u.Contrasena == Password).FirstOrDefault();

                if (usuario == null)
                {
                    return BadRequest("Credenciales incorrectas");
                }

                var Jwt = _Configuration.GetSection("Jwt").Get<Jwt>();

                var Claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, Jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UsuarioID", usuario.UsuarioId),
                new Claim("Correo", usuario.Correo),
                new Claim("Contrasena", usuario.Contrasena)
            };

                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.Key));
                var SignIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                var Token = new JwtSecurityToken(
                        Jwt.Issuer,
                        Jwt.Audience,
                        Claims,
                        expires: DateTime.Now.AddMinutes(3),
                        signingCredentials: SignIn
                    );

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Success", Respuesta = new JwtSecurityTokenHandler().WriteToken(Token) });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = ex.Message });
            }
            
        }
    }
}
