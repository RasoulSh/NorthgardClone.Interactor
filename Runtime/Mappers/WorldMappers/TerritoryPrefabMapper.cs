using System.Linq;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    internal class TerritoryPrefabMapper : IMapper<Territory, TerritoryPrefabViewModel>
    {
        [Inject] private IWorldPipelineService _worldPipeline;
        public TerritoryPrefabViewModel MapToTarget(Territory source)
        {
            return new TerritoryPrefabViewModel()
            {
                PrefabId = source.PrefabId,
                Title = source.Title
            };
        }

        public Territory MapToSource(TerritoryPrefabViewModel target)
        {
            return _worldPipeline.TerritoryPrefabs.First(tp => tp.PrefabId == target.PrefabId);
        }
    }
}