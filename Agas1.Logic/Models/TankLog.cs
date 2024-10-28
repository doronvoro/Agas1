namespace Agas1.Logic.Models
{
    public class TankLog
    {
        public int Id { get; set; }
        public int TankId { get; set; }
        public Tank Tank { get; set; }
        public OperationType Operation { get; set; }  // Enum for operations
        public int? TankProcessId { get; set; }  // Updated field name
        public TankProcess TankProcess { get; set; }  // Updated reference
        public int? MaterialId { get; set; }
        public Material Material { get; set; }
        public double VolumeChange { get; set; }
        public int? LiquidTypeId { get; set; }  // Nullable for transfers or processes
        public LiquidType LiquidType { get; set; }
        public int? SourceTankId { get; set; }  // Nullable for transfers
        public Tank SourceTank { get; set; }
        public DateTime Date { get; set; }
        public double VolumeBeforeChange { get;   set; }
    }
}
