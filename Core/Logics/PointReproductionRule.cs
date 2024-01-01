using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;
using Core.Logics.Base;
using Core.Models;
using Core.Models.Base;

namespace Core.Logics
{
    public class PointReproductionRule : ByTimeSimulationRule
    {
        public PointReproductionRule(int interval = 500) : base(interval) { }

        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            var rnd = new Random();
            foreach (var entity in space.GetEntities().Where(e => e is Point))
            {
                var point = (Point)entity;
                if (point.Hp < space.Config.PointHpForReproduction) continue;
                point.Hp -= space.Config.SubtractPointHpDuringReproduction;

                var child = CreatePointChild(point, space.Config, rnd);
                space.AddEntity(child);
            }

            return true;
        }

        private Point CreatePointChild(Point point, SimulationConfig config, Random? rnd = null)
        {
            rnd = rnd ?? new Random();
            var pointChild = new Point(point.Position, rnd.NextDouble() * Math.PI * 2, point.Type);

            pointChild.Hp = config.DefaultPointHp;
            pointChild.Velocity = point.Velocity;
            pointChild.VisibilityRange = point.VisibilityRange;

            return pointChild;
        }
    }   
}