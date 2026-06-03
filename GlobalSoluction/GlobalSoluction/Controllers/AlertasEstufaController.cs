using GlobalSoluction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 

namespace GlobalSoluction.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AlertasEstufaController : ControllerBase
{
    private readonly IAlertaEstufaService _alertaEstufaService;

    public AlertasEstufaController(IAlertaEstufaService alertaEstufaService)
    {
        _alertaEstufaService = alertaEstufaService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var alertas = await _alertaEstufaService.ListarTodosAsync();
        return Ok(alertas);
    }

    [HttpGet("estufa/{estufaConfigId}")]
    public async Task<IActionResult> ListarPorEstufa(int estufaConfigId)
    {
        var alertas = await _alertaEstufaService.ListarPorEstufaAsync(estufaConfigId);
        return Ok(alertas);
    }

    [HttpPatch("{id}/resolver")]
    public async Task<IActionResult> Resolver(int id)
    {
        var resolvido = await _alertaEstufaService.ResolverAsync(id);

        if (!resolvido)
        {
            return NotFound(new
            {
                mensagem = "Alerta não encontrado."
            });
        }

        return Ok(new
        {
            mensagem = "Alerta resolvido com sucesso."
        });
    }
}