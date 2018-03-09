using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Place Place { get; set; }

        [Required]
        public WatsonClassification Classification { get; set; }

        [Required]
        public byte[] ImageByteData { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
