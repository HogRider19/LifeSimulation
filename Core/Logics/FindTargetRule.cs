using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models;
using Core.Models.Base.Interfaces;

namespace Core.Logics
{
    public class FindTargetRule : ByTimeSimulationRule
    {
        private readonly ISimulationRule _contextRule;
        private readonly int _interval;

        public FindTargetRule(int interval, ISimulationRule contextRule) : base(interval)
        {
            _contextRule = contextRule;
            _interval = interval;
        }

        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            Console.WriteLine("Start");
            
            var context = state.GetRuleContext(_contextRule);
            var internalContext = state.GetRuleContext(this);
            
            if (!internalContext.ContainsKey("targetsTime"))
                internalContext.Add("targetsTime", new Dictionary<ISimulationEntity, long>());
            
            if (!context.ContainsKey("targets"))
                context.Add("targets", new Dictionary<ISimulationEntity, ISimulationEntity>());

            var entities = space.GetEntities().ToList(); 
            var targets = (Dictionary<ISimulationEntity, ISimulationEntity>)context["targets"];
            var targetsTime = (Dictionary<ISimulationEntity, long>)internalContext["targetsTime"];
            
            RemoveByCondition(targetsTime, p => !entities.Contains(p.Key));
            RemoveByCondition(targets, p => !entities.Contains(p.Key) || !entities.Contains(p.Value));
            
            Console.WriteLine("Start111");
            
            foreach (var entity in entities)
            {
                var currentUnixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                targetsTime.TryAdd(entity, currentUnixTime);

                if (currentUnixTime - targetsTime[entity] > _interval)
                {
                    targetsTime[entity] = currentUnixTime;

                    var target = GetNearlyTargetEntity(entity, entities);
                    if (target != null)
                        targets.Add(entity, target);
                }
            }
            
            return true;
        }

        protected static ISimulationEntity? GetNearlyTargetEntity(
            ISimulationEntity entity,
            IEnumerable<ISimulationEntity> spaceEntities)
        {
            var intendedTargets = spaceEntities.Where(e => IsTargetPair(entity, e));

            ISimulationEntity? target = null;
            var minDist = double.MaxValue;
            foreach (var variableIntendedTarget in intendedTargets)
            {
                var dist = entity.Position.GetDist(variableIntendedTarget.Position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = variableIntendedTarget;
                }
            }

            return target;
        }

        protected static bool IsTargetPair(ISimulationEntity e1, ISimulationEntity e2)
        {
            if (e1 is Point point1)
            {
                if (point1.Type == EPointType.Red)
                {
                    if (e2 is Meal)
                        return true;
                    return false;
                }

                if (e2 is Point point2)
                {
                    if (point1.Type == EPointType.Green && point2.Type == EPointType.Red)
                        return true;

                    if (point1.Type == EPointType.Blue && point2.Type == EPointType.Green)
                        return true;
                }
            }

            return false;
        }

        protected static void RemoveByCondition<TKey, TValue>(Dictionary<TKey, TValue> dict, Func<KeyValuePair<TKey, TValue>, bool> condition) where TKey : notnull
        {
            var itemsToRemove = dict.Where(condition).ToList();
            foreach (var item in itemsToRemove)
            {
                dict.Remove(item.Key);
            }
        }
    }    
}
