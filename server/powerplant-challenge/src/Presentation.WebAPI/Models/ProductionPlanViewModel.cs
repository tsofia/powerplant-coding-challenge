using System.Collections.Generic;

namespace Presentation.WebAPI.Models
{
    public class ProductionPlanViewModel
    {
        public int Load { get; set; }

        public FuelsViewModel Fuels { get; set; }
        
        public IEnumerable<PowerplantViewModel> Powerplants { get; set; }
    }
}