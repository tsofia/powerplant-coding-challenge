namespace Presentation.WebAPI.Models
{
    public class PowerplantViewModel
    {
        public string Name { get; set; }
        
        public PowerplantTypeViewModel Type { get; set; }
        
        public double Efficiency { get; set; }
        
        public int PMax { get; set; }
        
        public int PMin { get; set; }
    }
}