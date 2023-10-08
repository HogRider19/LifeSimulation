using Core.Models.Base.Interfaces;


namespace Core.Logics.Base.Interfaces
{
    public interface ISimulationRule
    {
        public ESimulationRuleType Type { get; }
        public bool Apply(ISimulationSpace space, ISimulationState state);
    }
}
