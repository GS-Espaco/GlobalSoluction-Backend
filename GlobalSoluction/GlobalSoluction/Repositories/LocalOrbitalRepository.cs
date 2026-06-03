using GlobalSoluction.Data;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSoluction.Repositories;

public class LocalOrbitalRepository : ILocalOrbitalRepository
{
    private readonly AppDbContext _context;

    public LocalOrbitalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LocalOrbital>> ListarTodosAsync()
    {
        return await _context.LocaisOrbitais
            .Include(local => local.Estufas)
            .ToListAsync();
    }

    public async Task<LocalOrbital?> BuscarPorIdAsync(int id)
    {
        return await _context.LocaisOrbitais
            .Include(local => local.Estufas)
            .FirstOrDefaultAsync(local => local.Id == id);
    }

    public async Task<LocalOrbital> CriarAsync(LocalOrbital localOrbital)
    {
        _context.LocaisOrbitais.Add(localOrbital);
        await _context.SaveChangesAsync();
        return localOrbital;
    }

    public async Task<LocalOrbital> AtualizarAsync(LocalOrbital localOrbital)
    {
        _context.LocaisOrbitais.Update(localOrbital);
        await _context.SaveChangesAsync();
        return localOrbital;
    }

    public async Task RemoverAsync(LocalOrbital localOrbital)
    {
        _context.LocaisOrbitais.Remove(localOrbital);
        await _context.SaveChangesAsync();
    }
}