namespace Agas1.Logic.Models
{
    public class TankLogRecord
    {
        public int Id { get; set; }
        public int TankId { get; set; }
        public string TankName { get; set; }
        public OperationType Operation { get; set; }  // Enum for operations
        public int? TankProcessId { get; set; }  // Updated field name
        public string ProcessName { get; set; }  // Updated reference
        public int? MaterialId { get; set; }
        public string MaterialName { get; set; }
        public double VolumeChange { get; set; }
        public int? LiquidTypeId { get; set; }  // Nullable for transfers or processes
        public string LiquidTypeName { get; set; }
        public int? SourceTankId { get; set; }  // Nullable for transfers
        public string SourceTankName { get; set; }
        public DateTime Date { get; set; }
        public double VolumeBeforeChange { get; set; }
        public string? TankProcessName { get;   set; }

        public string GetSourceDestinationName()
        {
            if (!string.IsNullOrEmpty(SourceTankName))
            {
                return $"Tank {SourceTankName}";
            }

            if (!string.IsNullOrEmpty(LiquidTypeName)) 
            {
                return LiquidTypeName;
            }

            if (!string.IsNullOrEmpty(TankProcessName))
            {
                var materialName = string.IsNullOrEmpty(MaterialName) ? string.Empty : $"[{MaterialName}]";
                return $"{TankProcessName}{materialName}";

                //var material = MaterialId.HasValue ? $"({Material.Name})" : string.Empty;
                //return $"{TankProcess.Name} {material}";

            }
            return "N/A";
        }
    }
}
