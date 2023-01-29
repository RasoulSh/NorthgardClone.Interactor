using JetBrains.Annotations;
using Northgard.Core.Abstraction.Localization;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    [UsedImplicitly]
    public class NaturalDistrictMapper : IReadOnlyMapper<NaturalDistrict, NaturalDistrictViewModel>
    {
        [Inject] private ILocalization _localization;
        
        public NaturalDistrictViewModel MapToTarget(NaturalDistrict source)
        {
            return new NaturalDistrictViewModel()
            {
                Id = source.id,
                Name = _localization.GetText(source.Title),
                Position = source.position,
                Rotation = source.rotation,
                NaturalResourceName = _localization.GetText(source.naturalResource.ToString())
            };
        }
    }
}