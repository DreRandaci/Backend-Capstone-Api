using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendWatsonApi.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }                
        
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
