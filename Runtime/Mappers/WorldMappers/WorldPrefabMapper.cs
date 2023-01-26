using System.Linq;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    internal class WorldPrefabMapper : IMapper<World, WorldPrefabViewModel>
    {
        [Inject] private IWorldPipelineService _worldPipeline;
        public WorldPrefabViewModel MapToTarget(World source)
        {
            return new WorldPrefabViewModel()
            {
                PrefabId = source.PrefabId,
                Title = source.Title
            };
        }

        public World MapToSource(WorldPrefabViewModel target)
        {
            return _worldPipeline.WorldPrefabs.First(wp => wp.PrefabId == target.PrefabId);
        }
    }
}