using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChatSystem.DAL.Models
{
    public enum RiskLevel
    {
        Low,
        Medium,
        High
    }
    public class Alert : Entity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public string Message { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RiskLevel RiskLevel { get; set; } // Low, Medium, High
        public double Probability { get; set; }            // نسبة الخطر من نموذج بايثون
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
