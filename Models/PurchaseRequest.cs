using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcureDemo.Models
{
    public class PurchaseRequest
    {
        public int Id { get; set; }

        [Required]
        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Justification { get; set; } = string.Empty;

        [Range(0, 999999999)]
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "MYR";

        [MaxLength(50)]
        public string Status { get; set; } = "Submitted"; // Submitted, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedByUpn { get; set; } = string.Empty;
    }
}
