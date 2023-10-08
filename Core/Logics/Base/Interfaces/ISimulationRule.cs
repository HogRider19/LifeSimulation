using Core.Models.Base.Interfaces;


namespace Core.Logics.Base.Interfaces
{
    public interface ISimulationRule
    {
        public bool Apply(ISimulationSpace space, ISimulationState state);
    }
}
