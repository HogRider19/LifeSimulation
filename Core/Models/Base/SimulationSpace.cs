using Core.Models.Base.Interfaces;

namespace Core.Models.Base
{
    public class SimulationSpace : ISimulationSpace
    {
        public Dictionary<int, ISimulationEntity> Entities { get; private set; } = new();
        public SimulationConfig Config { get; set; } = new SimulationConfig();
        public int Width { get; init; }
        public int Height { get; init; }

        private List<ISimulationEntity> _entitiesList;

        public SimulationSpace(int width = 100, int height = 100)
        {
            Width = 100;
            Height = 100;

            _entitiesList = Entities.Values.ToList();
        }
        
        public int AddEntity(ISimulationEntity entity, int? id = null)
        {
            if (id == null) 
            {
                do
                {
                    id = new Random().Next();
                }
                while (Entities.ContainsKey((int)id));
            }

            Entities.Add((int)id, entity);
            UpdateEntitiesList();
            return (int)id;
        }

        public ISimulationEntity GetEntity(int id)
        {
            return Entities[id];
        }

        public void RemoveEntity(ISimulationEntity entity)
        {
            Entities.Remove(Entities.First(c => c.Value == entity).Key);
            UpdateEntitiesList();
        }

        public void RemoveEntity(int id)
        {
            Entities.Remove(id);
            UpdateEntitiesList();
        }

        public void Reset()
        {
            Entities = new Dictionary<int, ISimulationEntity>();
            UpdateEntitiesList();
        }

        public IEnumerable<ISimulationEntity> GetEntities() => _entitiesList;

        private void UpdateEntitiesList() => _entitiesList = Entities.Values.ToList();
    }
}
