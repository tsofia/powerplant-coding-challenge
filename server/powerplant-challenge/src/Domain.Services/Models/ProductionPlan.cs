using System.Collections.Generic;

namespace Domain.Services.Models
{
    public class ProductionPlan
    {
        public int Load { get; set; }

        public Fuels Fuels { get; set; }
        
        public IEnumerable<Powerplant> Powerplants { get; set; }
    }
}