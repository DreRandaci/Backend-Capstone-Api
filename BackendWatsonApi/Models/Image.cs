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
        public string ImageUri { get; set; }
    }
}
