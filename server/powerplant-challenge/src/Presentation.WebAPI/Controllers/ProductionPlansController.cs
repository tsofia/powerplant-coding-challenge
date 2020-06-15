using System.Collections.Generic;
using AutoMapper;
using Domain.Services;
using Domain.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.WebAPI.Models;

namespace Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionPlansController : ControllerBase
    {
        private readonly ILogger<ProductionPlansController> _logger;
        private readonly IProductionPlanService _productionPlanService;
        private readonly IMapper _mapper;

        public ProductionPlansController(ILogger<ProductionPlansController> logger, IProductionPlanService productionPlanService, IMapper mapper)
        {
            _logger = logger;
            _productionPlanService = productionPlanService;
            _mapper = mapper;
        }

        /// <summary>
        /// Given a production plan, returns the calculated the power output for each powerplant.
        /// </summary>
        /// <param name="productionPlan">Production plan payload</param>
        /// <returns>Power delivered by each powerplant</returns>
        [HttpPost]
        public IEnumerable<PowerplantOutputPowerViewModel> Post(ProductionPlanViewModel productionPlan)
        {
            var mappedProductionPlan = _mapper.Map<ProductionPlan>(productionPlan);
            var result = _productionPlanService.CalculateOutputPlan(mappedProductionPlan);

            return _mapper.Map<IEnumerable<PowerplantOutputPowerViewModel>>(result);
        }
    }
}