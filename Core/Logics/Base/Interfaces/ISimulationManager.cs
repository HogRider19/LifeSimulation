

namespace Core.Logics.Base.Interfaces
{
    public interface ISimulationManager
    {
        public void AddRule(ISimulationRule rule);
        public void AddRule(IEnumerable<ISimulationRule> rules);
        
        public void Start(bool resetSpase = true);
        public bool SwitchPause();
        public void Stop();
    }   
}