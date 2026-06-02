namespace GlobalSoluction.Models;

public class LeituraSensor
{
    public int Id { get; set; }

    public int SensorId { get; set; }

    public Sensor? Sensor { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataLeitura { get; set; } = DateTime.Now;
}