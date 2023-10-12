using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Base.Interfaces
{
    public interface ISimulationEntity
    {
        Position Position { get; }
        double Direction { get; }
        public int Hp { get; }

        void Rotate(double angle);
        void Move(Position position);
        void MoveTo(Position position);
        void MoveToDirection(double dist);
    }
}
