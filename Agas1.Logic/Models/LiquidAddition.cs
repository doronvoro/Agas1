namespace Agas1.Logic.Models
{
    public class LiquidAddition
    {
        public int Id { get; set; }
        public int TankId { get; set; }
        public Tank Tank { get; set; }
        public int LiquidTypeId { get; set; }
        public LiquidType LiquidType { get; set; }
        public double VolumeAdded { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
