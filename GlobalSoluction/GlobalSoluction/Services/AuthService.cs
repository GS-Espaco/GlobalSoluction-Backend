using GlobalSoluction.Data;
using GlobalSoluction.DTOs.Auth;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlobalSoluction.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthRespostaDto?> RegistrarAsync(RegistrarUsuarioDto dto)
    {
        var emailExiste = await _context.Usuarios
            .AnyAsync(usuario => usuario.Email == dto.Email);

        if (emailExiste)
        {
            return null;
        }

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Role = dto.Role
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return GerarResposta(usuario);
    }

    public async Task<AuthRespostaDto?> LoginAsync(LoginDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Email == dto.Email);

        if (usuario == null)
        {
            return null;
        }

        var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);

        if (!senhaValida)
        {
            return null;
        }

        return GerarResposta(usuario);
    }

    private AuthRespostaDto GerarResposta(Usuario usuario)
    {
        return new AuthRespostaDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role,
            Token = GerarToken(usuario)
        };
    }

    private string GerarToken(Usuario usuario)
    {
        var jwtKey = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("A chave JWT não foi configurada.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Role)
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}