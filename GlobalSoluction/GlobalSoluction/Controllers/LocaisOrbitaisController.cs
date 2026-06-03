using GlobalSoluction.DTOs.LocalOrbital;
using GlobalSoluction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalSoluction.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LocaisOrbitaisController : ControllerBase
{
    private readonly ILocalOrbitalService _localOrbitalService;

    public LocaisOrbitaisController(ILocalOrbitalService localOrbitalService)
    {
        _localOrbitalService = localOrbitalService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var locais = await _localOrbitalService.ListarTodosAsync();
        return Ok(locais);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var local = await _localOrbitalService.BuscarPorIdAsync(id);

        if (local == null)
        {
            return NotFound(new
            {
                mensagem = "Local orbital não encontrado."
            });
        }

        return Ok(local);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarLocalOrbitalDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var localCriado = await _localOrbitalService.CriarAsync(dto);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = localCriado.Id },
            localCriado
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarLocalOrbitalDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var localAtualizado = await _localOrbitalService.AtualizarAsync(id, dto);

        if (localAtualizado == null)
        {
            return NotFound(new
            {
                mensagem = "Local orbital não encontrado."
            });
        }

        return Ok(localAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _localOrbitalService.RemoverAsync(id);

        if (!removido)
        {
            return NotFound(new
            {
                mensagem = "Local orbital não encontrado."
            });
        }

        return NoContent();
    }
}