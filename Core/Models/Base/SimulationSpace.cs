using Core.Models.Base.Interfaces;

namespace Core.Models.Base
{
    public class SimulationSpace : ISimulationSpace
    {
        public Dictionary<int, ISimulationEntity> Entities { get; private set; } = new();

        public int AddEntity(ISimulationEntity entity, int? id = null)
        {
            if (id == null) 
            {
                do
                {
                    id = new Random().Next();
                }
                while (!Entities.ContainsKey((int)id));
            }

            Entities.Add((int)id, entity);
            return (int)id;
        }

        public ISimulationEntity GetEntity(int id)
        {
            return Entities[id];
        }

        public void RemoveEntity(ISimulationEntity entity)
        {
            Entities.Remove(Entities.First(c => c.Value == entity).Key);
        }

        public void RemoveEntity(int id)
        {
            Entities.Remove(id);
        }

        public void Reset()
        {
            Entities = new Dictionary<int, ISimulationEntity>();
        }
    }
}
