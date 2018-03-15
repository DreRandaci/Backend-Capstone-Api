using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Models
{
    public class UserPostDetail
    {
        public string Address { get; set; }
        public string Notes { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public virtual ICollection<WatsonClassification> Classifications { get; set; }

        public int ImgId { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
