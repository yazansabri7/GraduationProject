using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatSystem.DTO.DtoRequests
{
    public class PatientMedicationRequest
    {
        
            [Required]
            [MaxLength(150)]
            public string Name { get; set; }

            [Required]
            [MaxLength(100)]
            public string Dosage { get; set; } 

            [Required]
            [MaxLength(100)]
            public string Frequency { get; set; } 
        
    }
}
