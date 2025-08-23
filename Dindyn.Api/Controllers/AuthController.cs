using Microsoft.AspNetCore.Mvc;

namespace Dindyn.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Aqui você irá chamar o caso de uso de login futuramente
            // Por enquanto, retorna um 200 OK genérico
            return Ok(new { message = "Login endpoint funcionando!" });
        }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
