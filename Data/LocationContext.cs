using Microsoft.EntityFrameworkCore;
using getVehicleLocationAPI.Model;

namespace getVehicleLocationAPI.Data
{
	public class LocationContext : DbContext
	{
		public LocationContext(DbContextOptions<LocationContext> options)
			: base(options)
		{
		}
		
		public DbSet<VehicleLocation> VehicleLocations { get; set; }
	}
}
