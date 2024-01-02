using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: Web.Services.<SpaceInfoHandler>F7FB16AFB13C410195475ADF0BFBF7D848D91BFA81CAF9E0705E9F0F687E0C419__EntityInfo[]")]
        public string GetJsonRepresentation(ISimulationSpace space)
        {
            var entities = space.GetEntities();
            var entitiesArray = entities as ISimulationEntity[] ?? entities.ToArray();
            var data = new List<EntityInfo>(entitiesArray.Count());
            
            foreach (var entity in entitiesArray)
            {
                var entityInfo = new EntityInfo();
                entityInfo.X = (float)entity.Position.X;
                entityInfo.Y = (float)entity.Position.Y;
                
                if (entity.GetType() == typeof(Meal))
                    entityInfo.Type = 3;
                else if (entity.GetType() == typeof(Point))
                {
                    var point = (Point)entity;
                    entityInfo.Type = point.Type switch
                    {
                        EPointType.Red => 0,
                        EPointType.Green => 1,
                        EPointType.Blue => 2,
                        _ => throw new AggregateException("Invalid the point type")
                    };
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