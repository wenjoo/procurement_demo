using System.ComponentModel.DataAnnotations;

namespace ProcureDemo.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ContactEmail { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active, Suspended
    }
}
