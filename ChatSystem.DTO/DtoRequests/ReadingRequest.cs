using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class ReadingRequest 
    {
        [Range(1, 500, ErrorMessage = "Invalid systolic value.")]
        public int? Systolic { get; set; }

        [Range(1, 300, ErrorMessage = "Invalid diastolic value.")]
        public int? Diastolic { get; set; }

        [Range(1, 1000, ErrorMessage = "Invalid glucose value.")]
        public double? GlucoseLevel { get; set; }

    }
}
