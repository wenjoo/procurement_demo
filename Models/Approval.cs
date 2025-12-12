namespace ProcureDemo.Models
{
    public class Approval
    {
        public int Id { get; set; }
        public int PurchaseRequestId { get; set; }
        public PurchaseRequest? PurchaseRequest { get; set; }

        public string ApproverUpn { get; set; } = string.Empty;
        public DateTime ActionAt { get; set; } = DateTime.UtcNow;
        public string Action { get; set; } = "Pending"; // Pending, Approved, Rejected
        public string? Comments { get; set; }
    }
}
