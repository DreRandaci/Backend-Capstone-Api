using System.ComponentModel.DataAnnotations;

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
