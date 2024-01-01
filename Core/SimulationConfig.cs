namespace Core;

public class SimulationConfig
{
    public int DefaultPointHp { get; set; } = 1000;
    public int DefaultPointVelocity { get; set; } = 2;
    public int DefaultPointVisibilityRange { get; set; } = 10;
    public int SubtractPointHpPerSecond { get; set; } = 10;
    
    public int PointHpForReproduction { get; set; } = 2000;
    public int SubtractPointHpDuringReproduction { get; set; } = 1000;
}