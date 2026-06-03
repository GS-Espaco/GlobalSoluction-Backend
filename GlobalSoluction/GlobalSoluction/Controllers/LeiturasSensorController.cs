using GlobalSoluction.DTOs.LeituraSensor;
using GlobalSoluction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalSoluction.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LeiturasSensorController : ControllerBase
{
    private readonly ILeituraSensorService _leituraSensorService;

    public LeiturasSensorController(ILeituraSensorService leituraSensorService)
    {
        _leituraSensorService = leituraSensorService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var leituras = await _leituraSensorService.ListarTodasAsync();
        return Ok(leituras);
    }

    [HttpGet("estufa/{estufaConfigId}")]
    public async Task<IActionResult> ListarPorEstufa(int estufaConfigId)
    {
        var leituras = await _leituraSensorService.ListarPorEstufaAsync(estufaConfigId);
        return Ok(leituras);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarLeituraSensorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var leituraCriada = await _leituraSensorService.CriarAsync(dto);

        if (leituraCriada == null)
        {
            return NotFound(new
            {
                mensagem = "Estufa não encontrada."
            });
        }

        return Created("", leituraCriada);
    }
}