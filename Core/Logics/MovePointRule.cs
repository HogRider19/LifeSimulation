using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models.Base.Interfaces;

namespace Core.Logics
{
    public class MovePointRule : SimulationRule
    {
        public MovePointRule() : base(ESimulationRuleType.EveryCycle) { }
        
        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            var context = state.GetRuleContext(this);
            var targets = (Dictionary<ISimulationEntity, ISimulationEntity>?)context["targets"];

            if (targets != null)
            {
                Console.WriteLine($"Targets count: {targets.Count}");
            }

            return true;
        }
    }   
}