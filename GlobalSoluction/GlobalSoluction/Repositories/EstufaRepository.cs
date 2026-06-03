using GlobalSoluction.Data;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSoluction.Repositories;

public class EstufaRepository : IEstufaRepository
{
    private readonly AppDbContext _context;

    public EstufaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EstufaConfig>> ListarTodasAsync()
    {
        return await _context.EstufasConfig
            .Include(estufa => estufa.LocalOrbital)
            .ToListAsync();
    }

    public async Task<EstufaConfig?> BuscarPorIdAsync(int id)
    {
        return await _context.EstufasConfig
            .Include(estufa => estufa.LocalOrbital)
            .FirstOrDefaultAsync(estufa => estufa.Id == id);
    }

    public async Task<EstufaConfig> CriarAsync(EstufaConfig estufa)
    {
        _context.EstufasConfig.Add(estufa);
        await _context.SaveChangesAsync();
        return estufa;
    }

    public async Task<EstufaConfig> AtualizarAsync(EstufaConfig estufa)
    {
        _context.EstufasConfig.Update(estufa);
        await _context.SaveChangesAsync();
        return estufa;
    }

    public async Task RemoverAsync(EstufaConfig estufa)
    {
        _context.EstufasConfig.Remove(estufa);
        await _context.SaveChangesAsync();
    }
}