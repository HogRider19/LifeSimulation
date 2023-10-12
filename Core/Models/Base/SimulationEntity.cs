using Core.Models.Base.Interfaces;

namespace Core.Models.Base
{
    public abstract class SimulationEntity : ISimulationEntity
    {
        public Position Position { get; protected set; }
        public double Direction { get; set; }
        public int Hp { get; set; } = 1000;

        public SimulationEntity(Position position = new Position(), double direction = 0)
        {
            Position = position;
            Direction = direction;
        }
        
        public void MoveToDirection(double dist)
        {
            Move(new Position(
                dist * Math.Cos(Direction),
                dist * Math.Sin(Direction)
            ));
        }

        public void Rotate(double angle) => Direction += angle;
        public void Move(Position position) => Position += position;
        public void MoveTo(Position position) => Position = position;
    }
}
