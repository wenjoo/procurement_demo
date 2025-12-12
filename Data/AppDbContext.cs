using Microsoft.EntityFrameworkCore;
using ProcureDemo.Models;

namespace ProcureDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Vendor> Vendors => Set<Vendor>();
        public DbSet<StaffUser> StaffUsers => Set<StaffUser>();
        public DbSet<PurchaseRequest> PurchaseRequests => Set<PurchaseRequest>();
        public DbSet<Approval> Approvals => Set<Approval>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
    }
}
