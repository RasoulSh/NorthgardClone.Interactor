using System.Collections.Generic;

namespace Northgard.Interactor.ViewModels.WorldViewModels
{
    public class TerritoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<NaturalDistrictViewModel> NaturalDistricts { get; set; }
    }
}