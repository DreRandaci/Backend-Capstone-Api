using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Models
{
    public class UserPost
    {
        [Key]
        public int UserPostId { get; set; }

        [Required]
        public User User { get; set; }                

        [Required]
        public int PlaceId { get; set; }

        public Place Place { get; set; }

        [Required]
        public int ImageId { get; set; }

        public Image Image { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public virtual ICollection<WatsonClassification> Classifications { get; set; }
    }
}
