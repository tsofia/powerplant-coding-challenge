using Newtonsoft.Json;

namespace Presentation.WebAPI.Models
{
    public class PowerplantOutputPowerViewModel
    {
        public string Name { get; set; }
        
        // At most, one decimal precision
        [JsonProperty("p")]
        public double Power { get; set; }
    }
}