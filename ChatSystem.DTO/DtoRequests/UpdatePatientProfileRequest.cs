using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class UpdatePatientProfileRequest : BaseProfileRequest
    {
        [Range(1, 300)]
        public double? Height { get; set; }

        [Range(1, 500)]
        public double? Weight { get; set; }
    }
}
