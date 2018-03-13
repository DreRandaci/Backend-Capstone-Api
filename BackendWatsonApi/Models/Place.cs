using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        
        public string Address { get; set; }
      
        public string Notes { get; set; }
        
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual ICollection<UserPost> PlaceImages { get; set; }
    }
}
