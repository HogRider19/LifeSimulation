using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models;
using Core.Models.Base;
using Core.Models.Base.Interfaces;

namespace Core.Logics
{
    public class HpHandlerRule : ByTimeSimulationRule
    {
        public HpHandlerRule(int interval = 1000) : base(interval) { }

        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            var entities = space.GetEntities().ToList();
            
            foreach (var entity in entities.Where(e => e is Point))
            {
                ((SimulationEntity)entity).Hp -= 100;
            }

            foreach (var entity in entities)
                if (entity.Hp <= 0)
                    space.RemoveEntity(entity);
            
            return true;
        }
    }   
}