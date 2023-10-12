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

                var unixTimeNow = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                moveTimes.TryAdd(entity, unixTimeNow);

                if (unixTimeNow - moveTimes[entity] > MoveInterval)
                {
                    var rnd = new Random();
                    
                    moveTimes[entity] = unixTimeNow;
                    point.Direction += (rnd.NextDouble() - 0.5) * Math.PI / 6;
                }

                point.MoveToDirection(point.Velocity / 10000);
            }

            return true;
        }
    }   
}