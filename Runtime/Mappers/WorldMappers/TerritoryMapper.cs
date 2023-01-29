﻿using System.Linq;
using JetBrains.Annotations;
using Northgard.Core.Abstraction.Localization;
using Northgard.GameWorld.Abstraction;
using Northgard.GameWorld.Entities;
using Northgard.Interactor.Common.Mapper;
using Northgard.Interactor.ViewModels.WorldViewModels;
using Zenject;

namespace Northgard.Interactor.Mappers.WorldMappers
{
    [UsedImplicitly]
    public class TerritoryMapper : IReadOnlyMapper<Territory, TerritoryViewModel>
    {
        [Inject] private ILocalization _localization;
        [Inject] private IReadOnlyMapper<NaturalDistrict, NaturalDistrictViewModel> _naturalDistrictMapper;
        [Inject] private IWorldPipelineService _pipelineService;
        
        public TerritoryViewModel MapToTarget(Territory source)
        {
            var naturalDistricts = source.naturalDistricts.Select(ndId =>
                _pipelineService.FindNaturalDistrict(ndId).Data);
            return new TerritoryViewModel()
            {
                Id = source.id,
                Name = _localization.GetText(source.Title),
                NaturalDistricts = naturalDistricts.Select(_naturalDistrictMapper.MapToTarget).ToList()
            };
        }
    }
}