using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Presentation.WebAPI.Models
{
    public class PowerplantViewModel
    {
        public string Name { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PowerplantTypeViewModel Type { get; set; }
        
        public double Efficiency { get; set; }
        
        public int PMax { get; set; }
        
        public int PMin { get; set; }
    }
}