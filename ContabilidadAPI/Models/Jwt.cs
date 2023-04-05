using System.Security.Claims;
using ContabilidadAPI.Controllers;
using ContabilidadAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadAPI.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

    }
}
