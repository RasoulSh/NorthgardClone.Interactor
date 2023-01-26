using System.Collections.Generic;

namespace Northgard.Interactor.ViewModels.WorldViewModels
{
    public class WorldViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TerritoryViewModel> Territories { get; set; }
    }
}