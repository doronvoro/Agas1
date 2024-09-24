using Agas1.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Agas1.Logic.Services
{
    public class LiquidTypeService
    {
        private readonly DistilleryContext _context;

        public LiquidTypeService(DistilleryContext context)
        {
            _context = context;
        }

        // Get all LiquidTypes
        public async Task<List<LiquidType>> GetAllLiquidTypesAsync()
        {
            return await _context.LiquidTypes.ToListAsync();
        }

        // Add a new LiquidType
        public async Task AddLiquidTypeAsync(LiquidType liquidType)
        {
            _context.LiquidTypes.Add(liquidType);
            await _context.SaveChangesAsync();
        }

        // Update an existing LiquidType
        public async Task UpdateLiquidTypeAsync(LiquidType liquidType)
        {
            _context.LiquidTypes.Update(liquidType);
            await _context.SaveChangesAsync();
        }

        // Delete a LiquidType by ID
        public async Task DeleteLiquidTypeAsync(int liquidTypeId)
        {
            var liquidType = await _context.LiquidTypes.FindAsync(liquidTypeId);
            if (liquidType != null)
            {
                _context.LiquidTypes.Remove(liquidType);
                await _context.SaveChangesAsync();
            }
        }
    }
}