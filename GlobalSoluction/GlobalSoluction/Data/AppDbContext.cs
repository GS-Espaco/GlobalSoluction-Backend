using GlobalSoluction.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSoluction.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<LocalOrbital> LocaisOrbitais { get; set; }
    public DbSet<EstufaConfig> EstufasConfig { get; set; }
    public DbSet<Sensor> Sensores { get; set; }
    public DbSet<LeituraSensor> LeiturasSensor { get; set; }
    public DbSet<AlertaEstufa> AlertasEstufa { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LocalOrbital>()
            .HasMany(local => local.Estufas)
            .WithOne(estufa => estufa.LocalOrbital)
            .HasForeignKey(estufa => estufa.LocalOrbitalId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EstufaConfig>()
            .HasMany(estufa => estufa.Sensores)
            .WithOne(sensor => sensor.EstufaConfig)
            .HasForeignKey(sensor => sensor.EstufaConfigId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EstufaConfig>()
            .HasMany(estufa => estufa.Alertas)
            .WithOne(alerta => alerta.EstufaConfig)
            .HasForeignKey(alerta => alerta.EstufaConfigId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sensor>()
            .HasMany(sensor => sensor.Leituras)
            .WithOne(leitura => leitura.Sensor)
            .HasForeignKey(leitura => leitura.SensorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sensor>()
            .HasMany(sensor => sensor.Alertas)
            .WithOne(alerta => alerta.Sensor)
            .HasForeignKey(alerta => alerta.SensorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Usuario>()
            .HasIndex(usuario => usuario.Email)
            .IsUnique();

        modelBuilder.Entity<LocalOrbital>()
            .Property(local => local.IncidenciaSolar)
            .HasPrecision(5, 2);

        modelBuilder.Entity<LocalOrbital>()
            .Property(local => local.NivelRiscoRadiacao)
            .HasPrecision(5, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.TemperaturaIdealMin)
            .HasPrecision(6, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.TemperaturaIdealMax)
            .HasPrecision(6, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.UmidadeArIdealMin)
            .HasPrecision(6, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.UmidadeArIdealMax)
            .HasPrecision(6, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.UmidadeSoloIdealMin)
            .HasPrecision(6, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.UmidadeSoloIdealMax)
            .HasPrecision(6, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.LuminosidadeIdealMin)
            .HasPrecision(8, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.LuminosidadeIdealMax)
            .HasPrecision(8, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.Co2IdealMin)
            .HasPrecision(8, 2);

        modelBuilder.Entity<EstufaConfig>()
            .Property(estufa => estufa.Co2IdealMax)
            .HasPrecision(8, 2);

        modelBuilder.Entity<LeituraSensor>()
            .Property(leitura => leitura.Valor)
            .HasPrecision(10, 2);
    }
}