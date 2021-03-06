﻿using System.ComponentModel.DataAnnotations;

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
        public string ConfidenceScore { get; set; }

        [Required]
        public int UserPostId { get; set; }

        public UserPost UserPost { get; set; }

        // Not all responses include a type hierarchy
        public string TypeHierarchy { get; set; }
    }
}
