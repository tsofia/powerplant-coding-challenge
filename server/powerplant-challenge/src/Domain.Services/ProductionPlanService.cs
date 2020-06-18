using System.Collections.Generic;
using System.Linq;
using Domain.Services.Exceptions;
using Domain.Services.Models;

namespace Domain.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        public IEnumerable<PowerplantOutputPower> CalculateOutputPlan(ProductionPlan productionPlan)
        {
            var outputPowerplants = new List<PowerplantOutputPower>();
            var accumulatedPower = 0.0;
         
            // These have higher priority because they are more efficient and cheaper
            var windTurbines = productionPlan.Powerplants.Where(x => x.Type == PowerplantType.WindTurbine);
            
            foreach (var windTurbine in windTurbines)
            {
                var calculatedWindTurbinePower = CalculateWindTurbineOutputPower(productionPlan.Fuels.Wind, windTurbine);
                var finalPower = 0.0;
                
                if (calculatedWindTurbinePower + accumulatedPower <= productionPlan.Load)
                {
                    finalPower = calculatedWindTurbinePower;
                    accumulatedPower += calculatedWindTurbinePower;
                }
                else
                {
                    finalPower = productionPlan.Load - accumulatedPower;
                }
                
                outputPowerplants.Add(new PowerplantOutputPower
                {
                    Name = windTurbine.Name,
                    Power = finalPower
                });
            }

            // Gas fired is the second cheapest and more efficient
            var gasTurbines = productionPlan.Powerplants.Where(x => x.Type == PowerplantType.GasFired);

            foreach (var gasTurbine in gasTurbines)
            {
                var leftOverLoad = productionPlan.Load - accumulatedPower;
                var calculatedGasTurbineOutputPower = CalculateNonWindTurbineOutputPower(gasTurbine);
                var finalPower = 0.0;
                
                if (gasTurbine.PMin * gasTurbine.Efficiency <= leftOverLoad)
                {
                    if (calculatedGasTurbineOutputPower + accumulatedPower <= productionPlan.Load)
                    {
                        finalPower = calculatedGasTurbineOutputPower;
                        accumulatedPower += calculatedGasTurbineOutputPower;
                    }
                    else
                    {
                        finalPower = leftOverLoad;
                        accumulatedPower += finalPower;
                    }
                }

                outputPowerplants.Add(new PowerplantOutputPower
                {
                    Name = gasTurbine.Name,
                    Power = finalPower
                });
            }
            
            // If the Gas fired turbine needs to output power, it needs to be at least PMin
            // If it is lower than PMin, then check other types
            // These have higher priority because they are more efficient and cheaper
            var turboJetTurbines = productionPlan.Powerplants.Where(x => x.Type == PowerplantType.TurboJet);
            
            foreach (var turboJetTurbine in turboJetTurbines)
            {
                var calculatedTurboJetTurbinePower = CalculateNonWindTurbineOutputPower(turboJetTurbine);
                var finalPower = 0.0;
                
                if (calculatedTurboJetTurbinePower + accumulatedPower <= productionPlan.Load)
                {
                    finalPower = calculatedTurboJetTurbinePower;
                    accumulatedPower += calculatedTurboJetTurbinePower;
                }
                else
                {
                    finalPower = productionPlan.Load - accumulatedPower;
                }
                
                outputPowerplants.Add(new PowerplantOutputPower
                {
                    Name = turboJetTurbine.Name,
                    Power = finalPower
                });
            }

            if (outputPowerplants.Sum(x => x.Power) < productionPlan.Load)
            {
                throw new InsufficientPowerException();
            }
            
            return outputPowerplants;
        }

        private double CalculateWindTurbineOutputPower(double wind, Powerplant powerplant)
        {
            return wind / 100 * powerplant.PMax;
        }
        
        private double CalculateNonWindTurbineOutputPower(Powerplant powerplant)
        {
            return powerplant.PMax * powerplant.Efficiency;
        }
    }
}