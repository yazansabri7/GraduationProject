using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Medication : Entity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser User { get; set; }

    }
}
