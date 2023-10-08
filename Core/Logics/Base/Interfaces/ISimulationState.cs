

namespace Core.Logics.Base.Interfaces
{
    public interface ISimulationState
    {
        public Dictionary<string, object> GetRuleContext(ISimulationRule rule);
        public void Clear(ISimulationRule rule);
    }
};
