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
        public async Task<Tank> FillTankFromExternalResource(int tankId, int liquidTypeId, double volume)
        {
            var tank = await _context.Tanks.FindAsync(tankId) ?? 
                throw new InvalidOperationException("Tank not found");
           
            var liquidType = await _context.LiquidTypes.FindAsync(liquidTypeId) ?? 
                throw new InvalidOperationException("Liquid type not found");

           
             
            // Log the operation in the TankLog
            var log = new TankLog
            {
                TankId = tankId,
                Operation = OperationType.Addition,  // Use enum
                VolumeBeforeChange = tank.Volume,
                VolumeChange = volume,
                LiquidTypeId = liquidTypeId,
                Date = DateTime.UtcNow
            };


            // Update tank volume
            tank.Volume += volume;

            _context.TankLogs.Add(log);

            await _context.SaveChangesAsync();

            return tank;
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



            // Log the transfer from the source tank
            var fromLog = new TankLog
            {
                TankId = fromTankId,
                Operation = OperationType.TransferOut,  // Use enum
                VolumeBeforeChange = fromTank.Volume,
                VolumeChange = -volume,
                LiquidTypeId = null,  // No specific liquid type for transfers (can be mixed)
                SourceTankId = toTankId,
                Date = DateTime.UtcNow
            };

            // Log the transfer to the destination tank
            var toLog = new TankLog
            {
                TankId = toTankId,
                Operation = OperationType.TransferIn,  // Use enum
                VolumeBeforeChange = toTank.Volume,
                VolumeChange = volume,
                LiquidTypeId = null,  // No specific liquid type for transfers (can be mixed)
                SourceTankId = fromTankId,
                Date = DateTime.UtcNow
            };

            _context.TankLogs.Add(fromLog);
            _context.TankLogs.Add(toLog);

            // Update tank volumes
            fromTank.Volume -= volume;
            toTank.Volume += volume;

            if (fromTank.Volume < 0 || toTank.Volume < 0)
            {
                throw new Exception("Invalid Volume");
            }

            await _context.SaveChangesAsync();
        }

        // Method to get all tank processes
        public async Task<List<TankProcess>> GetTankProcessesAsync()
        {
            return await _context.TankProcesses.ToListAsync();  // Updated to use TankProcess
        }
        public async Task<List<Material>> GetMaterialsAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        // Method to handle tank process logic
        public async Task<bool> ProcessTank(int tankId, int tankProcessId, double volumeChange, int? materialId = null)
        {
            var tank = await _context.Tanks.FindAsync(tankId);
            if (tank == null || volumeChange == 0)
            {
                return false;
            }

            var tankProcess = await _context.TankProcesses.FindAsync(tankProcessId);  // Updated to use TankProcess
            if (tankProcess == null)
            {
                return false;
            }

            Material material = null;
            if (materialId.HasValue)
            {
                material = await _context.Materials.FindAsync(materialId.Value);
            }

            double newVolume = tank.Volume + volumeChange;
            if (newVolume < 0)
            {
                return false; // Volume cannot be negative
            }

           
           // await _context.SaveChangesAsync();

            // Create a new TankLog record
            var tankLog = new TankLog
            {
                TankId = tankId,
                TankProcessId = tankProcessId,  // Updated to use TankProcessId
                MaterialId = materialId,
                VolumeBeforeChange = tank.Volume,
                VolumeChange = volumeChange,
                Operation = OperationType.TankProcess,
         
                Date = DateTime.UtcNow
            };

            // Update the tank's volume
            tank.Volume = newVolume;

            _context.TankLogs.Add(tankLog);
            await _context.SaveChangesAsync();

            return true; // Process successful
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
