using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using getVehicleLocationAPI.Data;
using getVehicleLocationAPI.Model;
using getVehicleLocationAPI.ServiceFunctionality;
using System;

namespace getVehicleLocationAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/VehicleLocations")]
    public class VehicleLocationsController : Controller
    {
        private readonly LocationContext _context;

        public VehicleLocationsController(LocationContext context)
        {
            _context = context;
         

        }

        // GET: api/VehicleLocations
        [HttpGet]
        public IEnumerable<VehicleLocation> GetAllVehicles()
        {
            GetRequests allVehicles = new GetRequests(_context);
            return allVehicles.GetVehicleList();
          
        }

        // GET: api/GeoLocation/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var vehicleLocation = await _context.VehicleLocations.SingleOrDefaultAsync(m => m.Id == id);


            if (vehicleLocation == null)
            {
                return NotFound();
            }

            var address = vehicleLocation.Address;

            if (!String.IsNullOrEmpty(address))
            {
                return Ok(address);
            }


            string[] latLongArray = vehicleLocation.VehicleLatLong.Split("#");

            GetRequests location = new GetRequests(_context);
            string answer = await location.ReturnLocation(latLongArray[1]);
            string saveAnswer = await location.SaveAddress(answer, vehicleLocation);

            return Ok(answer);
        }

    
        // PUT: api/VehicleLocations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleLocation([FromRoute] int id, [FromBody] VehicleLocation vehicleLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleLocation.Id)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VehicleLocations
        [HttpPost]
        public async Task<IActionResult> PostVehicleLocation([FromBody] VehicleDTO vehicleLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PostCheck checkIt = new PostCheck();
            string response = "";
           // string res = await checkIt.DoesItExist(_context, vehicleLocation);
        //     if (res.Equals("1"))
        //     {
        // //        response = await checkIt.AddIt(_context, vehicleLocation);
        //     }
            // else
            // {
            //     response = "Response:{ status: Failed, description: Vehicle data not saved because it alreayd exists.}";
            // }
            

            return Ok(response);
        }

        // DELETE: api/VehicleLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveChanges deleteCall = new ActiveChanges(_context);
            string val = await deleteCall.ChangeActive(id);

            return Ok(val);
        }

        private bool VehicleLocationExists(int id)
        {
            return _context.VehicleLocations.Any(e => e.Id == id);
        }
    }
}