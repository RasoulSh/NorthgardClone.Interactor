using System.Collections.Generic;
using Northgard.Interactor.Enums.WorldEnums;
using Northgard.Interactor.ViewModels.WorldViewModels;
using UnityEngine;

namespace Northgard.Interactor.Abstraction
{
    public interface IWorldEditorController
    {
        public IEnumerable<WorldPrefabViewModel> WorldPrefabs { get; }
        public IEnumerable<TerritoryPrefabViewModel> TerritoryPrefabs { get; }
        public IEnumerable<NaturalDistrictPrefabViewModel> NaturalDistrictPrefabs { get; }
        event TerritoryViewModel.TerritoryDelegate OnTerritoryAdded;
        event NaturalDistrictViewModel.NaturalDistrictDelegate OnNaturalDistrictAdded;
        void SelectWorld(SelectWorldViewModel selectData);
        TerritoryViewModel SelectFirstTerritory(SelectFirstTerritoryViewModel selectData);
        TerritoryViewModel NewTerritory(CreateTerritoryViewModel createData);
        NaturalDistrictViewModel NewNaturalDistrict(CreateNaturalDistrictViewModel createData);
        void SaveWorld(string savedName);
        WorldViewModel LoadWorld(string savedName);
        TC AddComponentToTerritory<TC>(string territoryId) where TC : Component;
        TC AddComponentToNaturalDistrict<TC>(string naturalDistrictId) where TC : Component;
        IEnumerable<WorldDirection> GetTerritoryAvailableDirections(string territoryId);
    }
}