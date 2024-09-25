using Agas1.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Agas1.Logic.Services
{
    public class DistilleryService
    {
        private readonly DistilleryContext _context;

        public DistilleryService(DistilleryContext context)
        {
            _context = context;
        }

        // Get all available liquid types for external sources (Water, Alcohol, etc.)
        public async Task<List<LiquidType>> GetLiquidTypesAsync()
        {
            return await _context.LiquidTypes.ToListAsync();
        }

        // Add liquid from external resource (like water) into a tank
        public async Task FillTankFromExternalResource(int tankId, int liquidTypeId, double volume)
        {
            var tank = await _context.Tanks.FindAsync(tankId);
            if (tank == null)
                throw new InvalidOperationException("Tank not found");

            var liquidType = await _context.LiquidTypes.FindAsync(liquidTypeId);
            if (liquidType == null)
                throw new InvalidOperationException("Liquid type not found");

            // Update tank volume
            tank.Volume += volume;

            // Log the liquid addition
            var addition = new LiquidAddition
            {
                TankId = tankId,
                LiquidTypeId = liquidTypeId,
                VolumeAdded = volume,
                DateAdded = DateTime.UtcNow
            };
            _context.LiquidAdditions.Add(addition);

            // Log the operation in the TankLog
            var log = new TankLog
            {
                TankId = tankId,
                Operation = OperationType.Addition,  // Use enum
                VolumeChange = volume,
                LiquidTypeId = liquidTypeId,
                Date = DateTime.UtcNow
            };
            _context.TankLogs.Add(log);

            await _context.SaveChangesAsync();
        }

        // Transfer liquid from one tank to another
        public async Task TransferLiquidBetweenTanks(int fromTankId, int toTankId, double volume)
        {
            var fromTank = await _context.Tanks.FindAsync(fromTankId);
            var toTank = await _context.Tanks.FindAsync(toTankId);

            if (fromTank == null || toTank == null)
                throw new InvalidOperationException("Tank not found");

            if (fromTank.Volume < volume)
                throw new InvalidOperationException("Insufficient volume in source tank");

            // Update tank volumes
            fromTank.Volume -= volume;
            toTank.Volume += volume;

            // Log the transfer from the source tank
            var fromLog = new TankLog
            {
                TankId = fromTankId,
                Operation = OperationType.TransferOut,  // Use enum
                VolumeChange = -volume,
                LiquidTypeId = null,  // No specific liquid type for transfers (can be mixed)
                SourceTankId = toTankId,
                Date = DateTime.UtcNow
            };
            _context.TankLogs.Add(fromLog);

            // Log the transfer to the destination tank
            var toLog = new TankLog
            {
                TankId = toTankId,
                Operation = OperationType.TransferIn,  // Use enum
                VolumeChange = volume,
                LiquidTypeId = null,  // No specific liquid type for transfers (can be mixed)
                SourceTankId = fromTankId,
                Date = DateTime.UtcNow
            };
            _context.TankLogs.Add(toLog);

            await _context.SaveChangesAsync();
        }

        // Create a process (e.g., distillation, maceration) that affects tank volume
        public async Task CreateProcess(int tankId, string processType, double volumeChange)
        {
            var tank = await _context.Tanks.FindAsync(tankId);
            if (tank == null)
                throw new InvalidOperationException("Tank not found");

            // Update tank volume based on process (e.g., volume reduction after distillation)
            tank.Volume += volumeChange;

            // Log the process operation
            var log = new TankLog
            {
                TankId = tankId,
                Operation = OperationType.Process,  // Use enum
                VolumeChange = volumeChange,
                LiquidTypeId = null,  // No specific liquid type for processes
                Date = DateTime.UtcNow
            };
            _context.TankLogs.Add(log);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Tank>> GetVisibleTanksAsync()
        {
            return await _context.Tanks.Where(t => t.IsVisible).ToListAsync();
        }
        // Get all tanks (visible and invisible)
        public async Task<List<Tank>> GetAllTanksAsync()
        {
            return await _context.Tanks.ToListAsync();
        }

        // Add a new tank
        public async Task AddTank(string name, double initialVolume)
        {
            var tank = new Tank { Name = name, Volume = initialVolume, IsVisible = true };
            _context.Tanks.Add(tank);
            await _context.SaveChangesAsync();
        }

        // Update an existing tank
        public async Task UpdateTank(Tank tank)
        {
            _context.Tanks.Update(tank);
            await _context.SaveChangesAsync();
        }

        public async Task<Tank> GetTankByIdAsync(int tankId)
        {
            return await _context.Tanks.FirstOrDefaultAsync(t => t.Id == tankId);
        }

        // Delete a tank if it is not used
        public async Task<bool> DeleteTankAsync(int tankId)
        {
            var tank = await _context.Tanks.Include(t => t.LiquidAdditions).FirstOrDefaultAsync(t => t.Id == tankId);
            if (tank == null || tank.LiquidAdditions.Count > 0)
            {
                // Tank is used in liquid additions or does not exist
                return false;
            }

            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get the history of all operations performed on a tank
        public async Task<List<TankLog>> GetTankHistoryAsync(int tankId)
        {
            return await _context.TankLogs
                .Include(t => t.LiquidType)
                .Where(t => t.TankId == tankId)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        // Get a report of all tanks and their current volume
        public async Task<List<Tank>> GetTanksReportAsync()
        {
            return await _context.Tanks.Include(t => t.LiquidAdditions).ToListAsync();
        }
    }
}
