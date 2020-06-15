namespace Domain.Services.Models
{
    public class Powerplant
    {
        public string Name { get; set; }
        
        public PowerplantType Type { get; set; }
        
        public double Efficiency { get; set; }
        
        public int PMax { get; set; }
        
        public int PMin { get; set; }
    }
}