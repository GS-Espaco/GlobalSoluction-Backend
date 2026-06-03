using GlobalSoluction.Data;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSoluction.Repositories;

public class AlertaEstufaRepository : IAlertaEstufaRepository
{
    private readonly AppDbContext _context;

    public AlertaEstufaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AlertaEstufa>> ListarTodosAsync()
    {
        return await _context.AlertasEstufa
            .Include(alerta => alerta.EstufaConfig)
            .ToListAsync();
    }

    public async Task<List<AlertaEstufa>> ListarPorEstufaAsync(int estufaConfigId)
    {
        return await _context.AlertasEstufa
            .Include(alerta => alerta.EstufaConfig)
            .Where(alerta => alerta.EstufaConfigId == estufaConfigId)
            .ToListAsync();
    }

    public async Task<AlertaEstufa?> BuscarPorIdAsync(int id)
    {
        return await _context.AlertasEstufa
            .Include(alerta => alerta.EstufaConfig)
            .FirstOrDefaultAsync(alerta => alerta.Id == id);
    }

    public async Task<AlertaEstufa> CriarAsync(AlertaEstufa alerta)
    {
        _context.AlertasEstufa.Add(alerta);
        await _context.SaveChangesAsync();
        return alerta;
    }

    public async Task<AlertaEstufa> AtualizarAsync(AlertaEstufa alerta)
    {
        _context.AlertasEstufa.Update(alerta);
        await _context.SaveChangesAsync();
        return alerta;
    }
}