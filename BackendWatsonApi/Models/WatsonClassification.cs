using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendWatsonApi.Models
{
    public class WatsonClassification
    {
        [Key]
        public int ClassificationId { get; set; }

        [Required]
        public string ClassifierId { get; set; }

        [Required]
        public string ClassifierName { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public int ConfidenceScore { get; set; }

        // Not all responses include a type hierarchy
        public string TypeHierarchy { get; set; }
    }
}
