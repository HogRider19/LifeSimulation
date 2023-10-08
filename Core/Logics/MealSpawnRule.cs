using Core.Logics.Base;
using Core.Logics.Base.Interfaces;
using Core.Models;
using Core.Models.Base;
using Core.Models.Base.Interfaces;


namespace Core.Logics
{
    public class MealSpawnRule : ByTimeSimulationRule
    {
        private readonly int _countMealPerInterval;
        
        public MealSpawnRule(int interval, int countMealPerInterval = 10) : base(interval)
        {
            _countMealPerInterval = countMealPerInterval;
        }

        public override bool Apply(ISimulationSpace space, ISimulationState state)
        {
            for (var i = 0; i < _countMealPerInterval; i++)
            {
                space.AddEntity(new Meal(
                    Position.CreateRandom(space.Width, space.Height), 0));
            }

            return true;
        }
    }   
}