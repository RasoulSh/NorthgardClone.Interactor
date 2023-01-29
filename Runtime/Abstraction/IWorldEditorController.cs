using System.Collections.Generic;
using Northgard.Interactor.ViewModels.WorldViewModels;

namespace Northgard.Interactor.Abstraction
{
    public interface IWorldEditorController
    {
        public IEnumerable<WorldPrefabViewModel> WorldPrefabs { get; }
        public IEnumerable<TerritoryPrefabViewModel> TerritoryPrefabs { get; }
        public IEnumerable<NaturalDistrictPrefabViewModel> NaturalDistrictPrefabs { get; }
        void SelectWorld(SelectWorldViewModel selectData);
        TerritoryViewModel SelectFirstTerritory(SelectFirstTerritoryViewModel selectData);
        TerritoryViewModel NewTerritory(CreateTerritoryViewModel createData);
        NaturalDistrictViewModel NewNaturalDistrict(CreateNaturalDistrictViewModel createData);
        void SaveWorld(string savedName);
        WorldViewModel LoadWorld(string savedName);
    }
}