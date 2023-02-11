using Northgard.Core.Enums;

namespace Northgard.Interactor.ViewModels.WorldViewModels
{
    public class CreateTerritoryViewModel
    {
        public TerritoryPrefabViewModel Prefab { get; set; }
        public string SourceTerritoryId { get; set; }
        public WorldDirection Direction { get; set; }
    }
}