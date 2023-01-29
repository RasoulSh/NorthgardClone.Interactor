using System.Linq;
using JetBrains.Annotations;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    [UsedImplicitly]
    public class WorldMapper : IReadOnlyMapper<World, WorldViewModel>
    {
        [Inject] private IReadOnlyMapper<Territory, TerritoryViewModel> _territoryMapper;
        [Inject] private IWorldPipelineService _pipelineService;
        
        public WorldViewModel MapToTarget(World source)
        {
            var territories = source.territories.Select(tId => _pipelineService.FindTerritory(tId).Data);
            return new WorldViewModel()
            {
                Id = source.id,
                Name = source.Title,
                Territories = territories.Select(_territoryMapper.MapToTarget).ToList()
            };
        }
    }
}