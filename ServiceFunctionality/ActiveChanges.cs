using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using getVehicleLocationAPI.Data;
using Microsoft.EntityFrameworkCore;
using getVehicleLocationAPI.Model;

namespace getVehicleLocationAPI.ServiceFunctionality
{
    public class ActiveChanges
    {
        private readonly LocationContext _context;
        public ActiveChanges(LocationContext context)
        {
            _context = context;
        } 
        public async Task<string> ChangeActive(int id)
        {
            var vehicleLocation = await _context.VehicleLocations.SingleOrDefaultAsync(m => m.Id == id);
            if (vehicleLocation != null)
            {
                vehicleLocation.Active = 0;
            }
            _context.Entry(vehicleLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleLocationExists(id))
                {
                    return "not Found";
                }
                else
                {
                    throw;
                }
            }
            return "worked";
        }
        private bool VehicleLocationExists(int id)
        {
            return _context.VehicleLocations.Any(e => e.Id == id);
        }
    }
}
