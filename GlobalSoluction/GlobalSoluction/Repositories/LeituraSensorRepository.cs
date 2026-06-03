using GlobalSoluction.Data;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSoluction.Repositories;

public class LeituraSensorRepository : ILeituraSensorRepository
{
    private readonly AppDbContext _context;

    public LeituraSensorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LeituraSensor>> ListarTodasAsync()
    {
        return await _context.LeiturasSensor
            .Include(leitura => leitura.EstufaConfig)
            .ToListAsync();
    }

    public async Task<List<LeituraSensor>> ListarPorEstufaAsync(int estufaConfigId)
    {
        return await _context.LeiturasSensor
            .Include(leitura => leitura.EstufaConfig)
            .Where(leitura => leitura.EstufaConfigId == estufaConfigId)
            .ToListAsync();
    }

    public async Task<LeituraSensor> CriarAsync(LeituraSensor leitura)
    {
        _context.LeiturasSensor.Add(leitura);
        await _context.SaveChangesAsync();
        return leitura;
    }
}