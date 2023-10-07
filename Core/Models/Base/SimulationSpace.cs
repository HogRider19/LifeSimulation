using Core.Models.Base.Interfaces;

namespace Core.Models.Base
{
    public class SimulationSpace : ISimulationSpace
    {
        public Dictionary<int, ISimulationEntity> Entitis { get; private set; } = new();

        public int AddEntity(ISimulationEntity entity, int? id = null)
        {
            if (id == null) 
            {
                do
                {
                    id = new Random().Next();
                }
                while (!Entitis.ContainsKey((int)id));
            }

            Entitis.Add((int)id, entity);
            return (int)id;
        }

        public ISimulationEntity GetEntity(int id)
        {
            return Entitis[id];
        }

        public void RemoveEntity(ISimulationEntity entity)
        {
            Entitis.Remove(Entitis.First(c => c.Value == entity).Key);
        }

        public void RemoveEntity(int id)
        {
            Entitis.Remove(id);
        }
    }
}
