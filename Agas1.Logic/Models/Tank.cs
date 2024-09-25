namespace Agas1.Logic.Models
{
    public class Tank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Volume { get; set; }

        public bool IsVisible { get; set; } = true;
        public List<LiquidAddition> LiquidAdditions { get; set; } = new List<LiquidAddition>();
    }
}
