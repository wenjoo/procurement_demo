namespace ProcureDemo.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int PurchaseRequestId { get; set; }
        public PurchaseRequest? PurchaseRequest { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty; // demo local path
        public decimal Amount { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string UploadedByUpn { get; set; } = string.Empty;
    }
}
