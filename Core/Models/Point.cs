using Core.Models.Base;

namespace Core.Models
{
    public class Point : SimulationEntity 
    {
        public EPointType Type { get; protected set; }

        public Point(
            Position position,
            double direction,
            EPointType type = EPointType.Green) : base(position, direction)
        {
            Type = type;
        }
    }

    public enum EPointType { Red = 0, Green = 1, Blue = 2 }
}
