using GlobalSoluction.DTOs.Auth;
using GlobalSoluction.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalSoluction.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resposta = await _authService.RegistrarAsync(dto);

        if (resposta == null)
        {
            return BadRequest(new
            {
                mensagem = "E-mail já cadastrado."
            });
        }

        return Ok(resposta);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resposta = await _authService.LoginAsync(dto);

        if (resposta == null)
        {
            return Unauthorized(new
            {
                mensagem = "E-mail ou senha inválidos."
            });
        }

        return Ok(resposta);
    }
}