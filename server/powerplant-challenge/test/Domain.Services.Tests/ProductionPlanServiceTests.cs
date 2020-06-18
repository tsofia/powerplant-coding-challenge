using System.Collections.Generic;
using System.Linq;
using Domain.Services.Exceptions;
using Domain.Services.Models;
using NUnit.Framework;

namespace Domain.Services.Tests
{
    public class ProductionPlanServiceTests
    {
        private IProductionPlanService _productionPlanService;
        
        [SetUp]
        public void Setup()
        {
            _productionPlanService = new ProductionPlanService();
        }

        [Test]
        public void CalculateOutputPlan_ProductionPlanOnlyOneWindTurbine_ReturnsListOfOneWithAllLoad()
        {
            // Arrange
            var productionPlan = new ProductionPlan
            {
                Load = 400,
                Fuels = new Fuels
                {
                    Gas = 13.4,
                    Kerosine = 50.8,
                    Co2 = 20,
                    Wind = 100
                },
                Powerplants = new List<Powerplant>
                {
                    new Powerplant
                    {
                        Name = "wind1",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 400
                    }
                }
            };
            
            // Act
            var result = _productionPlanService.CalculateOutputPlan(productionPlan);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("wind1", result.FirstOrDefault().Name);
            Assert.AreEqual(400, result.FirstOrDefault().Power);
        }
        
        [Test]
        public void CalculateOutputPlan_ProductionPlanTwoWindTurbines_ReturnsListWithBalancedLoad()
        {
            // Arrange
            var productionPlan = new ProductionPlan
            {
                Load = 400,
                Fuels = new Fuels
                {
                    Gas = 13.4,
                    Kerosine = 50.8,
                    Co2 = 20,
                    Wind = 50
                },
                Powerplants = new List<Powerplant>
                {
                    new Powerplant
                    {
                        Name = "wind1",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 400
                    },
                    new Powerplant
                    {
                        Name = "wind2",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 400
                    }
                }
            };
            
            // Act
            var result = _productionPlanService.CalculateOutputPlan(productionPlan);

            // Assert
            Assert.AreEqual(2, result.Count());
            
            var wind1 = result.FirstOrDefault(x => x.Name == "wind1");
            Assert.IsNotNull(wind1);
            Assert.AreEqual(200, wind1.Power);
            
            var wind2 = result.FirstOrDefault(x => x.Name == "wind2");
            Assert.IsNotNull(wind2);
            Assert.AreEqual(200, wind2.Power);
        }
        
        [Test]
        public void CalculateOutputPlan_ExamplePayload1_ReturnsListWithLoadsEqualToProvidedLoad()
        {
            // Arrange
            var productionPlan = new ProductionPlan
            {
                Load = 480,
                Fuels = new Fuels
                {
                    Gas = 13.4,
                    Kerosine = 50.8,
                    Co2 = 20,
                    Wind = 60
                },
                Powerplants = new List<Powerplant>
                {
                    new Powerplant
                    {
                        Name = "gasfiredbig1",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.53,
                        PMin = 100,
                        PMax = 460
                    },
                    new Powerplant
                    {
                        Name = "gasfiredbig2",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.53,
                        PMin = 100,
                        PMax = 460
                    },
                    new Powerplant
                    {
                        Name = "gasfiredsomewhatsmaller",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.37,
                        PMin = 40,
                        PMax = 210
                    },
                    new Powerplant
                    {
                        Name = "tj1",
                        Type = PowerplantType.TurboJet,
                        Efficiency = 0.3,
                        PMin = 0,
                        PMax = 16
                    },
                    new Powerplant
                    {
                        Name = "windpark1",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 150
                    },
                    new Powerplant
                    {
                        Name = "windpark2",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 36
                    }
                }
            };
            
            // Act
            var result = _productionPlanService.CalculateOutputPlan(productionPlan);

            // Assert
            var resultingLoad = result.Sum(x => x.Power);
            Assert.AreEqual(productionPlan.Load, resultingLoad);
        }
        
        [Test]
        public void CalculateOutputPlan_ExamplePayload2_ReturnsListWithLoadsEqualToProvidedLoad()
        {
            // Arrange
            var productionPlan = new ProductionPlan
            {
                Load = 480,
                Fuels = new Fuels
                {
                    Gas = 13.4,
                    Kerosine = 50.8,
                    Co2 = 20,
                    Wind = 0
                },
                Powerplants = new List<Powerplant>
                {
                    new Powerplant
                    {
                        Name = "gasfiredbig1",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.53,
                            PMin = 100,
                            PMax = 460
                        },
                    new Powerplant
                        {
                            Name = "gasfiredbig2",
                            Type = PowerplantType.GasFired,
                            Efficiency = 0.53,
                            PMin = 100,
                            PMax = 460
                        },
                    new Powerplant
                        {
                            Name = "gasfiredsomewhatsmaller",
                            Type = PowerplantType.GasFired,
                            Efficiency = 0.37,
                            PMin = 40,
                            PMax = 210
                        },
                    new Powerplant
                        {
                            Name = "tj1",
                            Type = PowerplantType.TurboJet,
                            Efficiency = 0.3,
                            PMin = 0,
                            PMax = 16
                        },
                    new Powerplant
                        {
                            Name = "windpark1",
                            Type = PowerplantType.WindTurbine,
                            Efficiency = 1,
                            PMin = 0,
                            PMax = 150
                        },
                    new Powerplant
                        {
                            Name = "windpark2",
                            Type = PowerplantType.WindTurbine,
                            Efficiency = 1,
                            PMin = 0,
                            PMax = 36
                        }
                }
            };
            
            // Act
            var result = _productionPlanService.CalculateOutputPlan(productionPlan);

            // Assert
            var resultingLoad = result.Sum(x => x.Power);
            Assert.AreEqual(productionPlan.Load, resultingLoad);
        }
        
        [Test]
        public void CalculateOutputPlan_ExamplePayload3_ReturnsListWithLoadsEqualToProvidedLoad()
        {
            // Arrange
            var productionPlan = new ProductionPlan
            {
                Load = 910,
                Fuels = new Fuels
                {
                    Gas = 13.4,
                    Kerosine = 50.8,
                    Co2 = 20,
                    Wind = 60
                },
                Powerplants = new List<Powerplant>
                {
                    new Powerplant
                    {
                        Name = "gasfiredbig1",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.53,
                        PMin = 100,
                        PMax = 460
                    },
                    new Powerplant
                    {
                        Name = "gasfiredbig2",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.53,
                        PMin = 100,
                        PMax = 460
                    },
                    new Powerplant
                    {
                        Name = "gasfiredsomewhatsmaller",
                        Type = PowerplantType.GasFired,
                        Efficiency = 0.37,
                        PMin = 40,
                        PMax = 210
                    },
                    new Powerplant
                    {
                        Name = "tj1",
                        Type = PowerplantType.TurboJet,
                        Efficiency = 0.3,
                        PMin = 0,
                        PMax = 16
                    },
                    new Powerplant
                    {
                        Name = "windpark1",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 150
                    },
                    new Powerplant
                    {
                        Name = "windpark2",
                        Type = PowerplantType.WindTurbine,
                        Efficiency = 1,
                        PMin = 0,
                        PMax = 36
                    }
                }
            };
            
            // Act & Assert
            Assert.Throws<InsufficientPowerException>(() => _productionPlanService.CalculateOutputPlan(productionPlan));
        }
    }
}