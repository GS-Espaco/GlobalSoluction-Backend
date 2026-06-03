using GlobalSoluction.DTOs.Estufa;
using GlobalSoluction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalSoluction.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EstufasController : ControllerBase
{
    private readonly IEstufaService _estufaService;

    public EstufasController(IEstufaService estufaService)
    {
        _estufaService = estufaService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var estufas = await _estufaService.ListarTodasAsync();
        return Ok(estufas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var estufa = await _estufaService.BuscarPorIdAsync(id);

        if (estufa == null)
        {
            return NotFound(new
            {
                mensagem = "Estufa não encontrada."
            });
        }

        return Ok(estufa);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarEstufaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var estufaCriada = await _estufaService.CriarAsync(dto);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = estufaCriada.Id },
            estufaCriada
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarEstufaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var estufaAtualizada = await _estufaService.AtualizarAsync(id, dto);

        if (estufaAtualizada == null)
        {
            return NotFound(new
            {
                mensagem = "Estufa não encontrada."
            });
        }

        return Ok(estufaAtualizada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removida = await _estufaService.RemoverAsync(id);

        if (!removida)
        {
            return NotFound(new
            {
                mensagem = "Estufa não encontrada."
            });
        }

        return NoContent();
    }
}