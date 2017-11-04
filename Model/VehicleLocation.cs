using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace getVehicleLocationAPI.Model
{
    public class VehicleLocation
    {
        public int Id { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Range(0,1)]
        public int Active { get; set; }
        public string VehicleLatLong { get; set; }

    }
}
