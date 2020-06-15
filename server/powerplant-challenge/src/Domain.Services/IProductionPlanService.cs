using System.Collections.Generic;
using Domain.Services.Models;

namespace Domain.Services
{
    public interface IProductionPlanService
    {
        IEnumerable<PowerplantOutputPower> CalculateOutputPlan(ProductionPlan productionPlan);
    }
}