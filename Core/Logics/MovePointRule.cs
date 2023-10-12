using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models;
using Core.Models.Base.Interfaces;

namespace Core.Logics
{
    public class MovePointRule : SimulationRule
    {
        private const int MoveInterval = 100;
        
        public MovePointRule() : base(ESimulationRuleType.EveryCycle) { }
        
        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            var context = state.GetRuleContext(this);
            context.TryAdd("moveTimes", new Dictionary<ISimulationEntity, long>());
            
            var targets = (Dictionary<ISimulationEntity, ISimulationEntity>?)context.GetValueOrDefault("targets");
            var moveTimes = (Dictionary<ISimulationEntity, long>)context["moveTimes"];
            
            foreach (var entity in space.GetEntities().Where(e => e is Point))
            {
                var point = (Point)entity;

                if (targets != null && targets.ContainsKey( point)!)
                {
                    var targetPosition = targets[point].Position;
                    var pointPosition = point.Position;
                    point.Direction = Math.Atan2(
                        targetPosition.Y - pointPosition.Y,
                        targetPosition.X - pointPosition.X
                    );
                }
                else
                {
                    var unixTimeNow = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    moveTimes.TryAdd(point, unixTimeNow);

                    if (unixTimeNow - moveTimes[point] > MoveInterval)
                    {
                        var rnd = new Random();

                        moveTimes[point] = unixTimeNow;
                        point.Direction += (rnd.NextDouble() - 0.5) * Math.PI / 6;
                    }
                }
                
                point.MoveToDirection(point.Velocity / 10000);
            }

            return true;
        }
    }   
}