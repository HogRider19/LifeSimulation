using Core.Models;
using Core.Models.Base.Interfaces;
using Newtonsoft.Json;

namespace Web.Services
{
    public class SpaceInfoHandler
    {
        private readonly ILogger<SpaceInfoHandler> _logger;

        public SpaceInfoHandler(ILogger<SpaceInfoHandler> logger)
        {
            _logger = logger;
        }

        public string GetJsonRepresentation(ISimulationSpace space) 
        {
            var data = new List<EntityInfo>();
            foreach (var entity in space.GetEntities())
            {
                var entityInfo = new EntityInfo();
                entityInfo.X = (float)entity.Position.X;
                entityInfo.Y = (float)entity.Position.Y;
                
                if (entity.GetType() == typeof(Meal))
                    entityInfo.Type = 3;
                else if (entity.GetType() == typeof(Point))
                {
                    var point = (Point)entity;
                    entityInfo.Type = Convert.ToInt32(point.Type);
                }
                else
                    throw new ArgumentException("Undefined entity type");
                
                data.Add(entityInfo);
            }
            
            return JsonConvert.SerializeObject(data);
        }
    }

    file struct EntityInfo
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Type { get; set; }
    }
}