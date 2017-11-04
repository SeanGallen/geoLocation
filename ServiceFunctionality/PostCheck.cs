using getVehicleLocationAPI.Data;
using getVehicleLocationAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace getVehicleLocationAPI.ServiceFunctionality
{
	public class PostCheck
	{
		private LocationContext _context;

		public async Task<string> AddIt(LocationContext context, VehicleLocation vehicleLocation)
		{
			_context = context;
			_context.VehicleLocations.Add(vehicleLocation);
			await _context.SaveChangesAsync();

			string vehicleLatLong = "";
			vehicleLatLong += vehicleLocation.VehicleId;
			vehicleLatLong += "#";
			vehicleLatLong += vehicleLocation.Latitude;
			vehicleLatLong += ",";
			vehicleLatLong += vehicleLocation.Longitude;

			vehicleLocation.VehicleLatLong = vehicleLatLong;
			_context.Entry(vehicleLocation).State = EntityState.Modified;
			_context.VehicleLocations.Update(vehicleLocation);

			try
			{

				await _context.SaveChangesAsync();
				return "Response:{ status: Succes, description: Vehicle data saved}";
			}
			catch (DbUpdateConcurrencyException e)
			{
				return "Look here " + e.Message;

			}
		}

		public async Task<string> DoesItExist(LocationContext context, VehicleLocation vehicleLocation)
		{
			_context = context;
			var response = await _context.VehicleLocations.SingleOrDefaultAsync(m => m.VehicleLatLong == vehicleLocation.VehicleLatLong);

			if (response == null)
			{
				return "1";
			}
			return "0";
		}
		private bool VehicleLocationExists(string id)
		{
			return _context.VehicleLocations.Any(e => e.VehicleLatLong == id);
		}

	}
}
