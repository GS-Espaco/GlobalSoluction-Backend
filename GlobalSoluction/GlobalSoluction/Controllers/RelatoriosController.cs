using GlobalSoluction.Data;
using GlobalSoluction.DTOs.Relatorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GlobalSoluction.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly AppDbContext _context;

    public RelatoriosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("resumo")]
    public async Task<IActionResult> GerarResumo()
    {
        try
        {
            var relatorio = new RelatorioEstufaDto
            {
                TotalLocaisOrbitais = await _context.LocaisOrbitais.CountAsync(),
                TotalEstufas = await _context.EstufasConfig.CountAsync(),
                TotalLeituras = await _context.LeiturasSensor.CountAsync(),
                TotalAlertas = await _context.AlertasEstufa.CountAsync(),
                AlertasPendentes = await _context.AlertasEstufa.CountAsync(a => !a.Resolvido),
                AlertasResolvidos = await _context.AlertasEstufa.CountAsync(a => a.Resolvido),
                DataGeracao = DateTime.Now
            };

            return Ok(relatorio);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                mensagem = "Erro ao gerar relatório.",
                detalhe = ex.Message
            });
        }
    }

    [HttpGet("exportar-json")]
    public async Task<IActionResult> ExportarJson()
    {
        try
        {
            var alertas = await _context.AlertasEstufa
                .Select(alerta => new
                {
                    alerta.Id,
                    alerta.EstufaConfigId,
                    alerta.TipoSensor,
                    alerta.TipoAlerta,
                    alerta.NivelCriticidade,
                    alerta.Mensagem,
                    alerta.Recomendacao,
                    alerta.Resolvido,
                    alerta.DataCriacao,
                    alerta.DataResolucao
                })
                .ToListAsync();

            var json = JsonSerializer.Serialize(alertas, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            return File(
                System.Text.Encoding.UTF8.GetBytes(json),
                "application/json",
                "relatorio-alertas-global-soluction.json"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                mensagem = "Erro ao exportar relatório em JSON.",
                detalhe = ex.Message
            });
        }
    }
}