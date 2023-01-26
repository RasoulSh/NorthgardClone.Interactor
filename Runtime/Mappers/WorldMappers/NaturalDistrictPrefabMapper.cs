using System.Linq;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    internal class NaturalDistrictPrefabMapper : IMapper<NaturalDistrict, NaturalDistrictPrefabViewModel>
    {
        [Inject] private IWorldPipelineService _worldPipeline;
        public NaturalDistrictPrefabViewModel MapToTarget(NaturalDistrict source)
        {
            return new NaturalDistrictPrefabViewModel()
            {
                PrefabId = source.PrefabId,
                Title = source.Title
            };
        }

        public NaturalDistrict MapToSource(NaturalDistrictPrefabViewModel target)
        {
            return _worldPipeline.NaturalDistrictPrefabs.First(ndp => ndp.PrefabId == target.PrefabId);
        }
    }
}