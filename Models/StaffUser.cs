namespace ProcureDemo.Models
{
    public class StaffUser
    {
        public int Id { get; set; }
        public string UPN { get; set; } = string.Empty; // user@company.com
        public string DisplayName { get; set; } = string.Empty;
        public string Role { get; set; } = "Staff"; // Staff, Approver, Finance, Admin
    }
}
