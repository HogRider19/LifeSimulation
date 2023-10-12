using Core.Models.Base;

namespace Core.Models
{
    public class Point : SimulationEntity 
    {
        public EPointType Type { get; protected set; }
        public double Velocity { get; set; }


        public Point(
            Position position,
            double direction,
            EPointType type = EPointType.Red) : base(position, direction)
        {
            Type = type;
            Hp = 1000;
            Velocity = 2;
        }
    }

    public enum EPointType { Red = 0, Green = 1, Blue = 2 }
}
