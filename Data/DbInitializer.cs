using System.Collections.Generic;
using System.Linq;
using getVehicleLocationAPI.Model;

namespace getVehicleLocationAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LocationContext context)
        {
            context.Database.EnsureCreated();

            if (context.VehicleLocations.Any())
            {
                return;
            }
            context.VehicleLocations.Add(new VehicleLocation { VehicleId = 1, Latitude = 35, Longitude = -23.22, Active = 1, VehicleLatLong = "1#42.914598,-91.617190" });
            context.VehicleLocations.Add(new VehicleLocation { VehicleId = 2, Latitude = 305, Longitude = -23.99, Active = 0, VehicleLatLong = "1#-91.617190,42.914598" });
            context.VehicleLocations.Add(new VehicleLocation { VehicleId = 3, Latitude = 42.914598, Longitude = -91.617190, Active = 1, VehicleLatLong = "1#40.714224,-73.961452" });

            context.SaveChangesAsync();
        }
    }
}