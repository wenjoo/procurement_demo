using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProcureDemo.Data;
using ProcureDemo.Models;
using ProcureDemo.Services;

public class ApprovalsModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly AuditService _audit;

    public ApprovalsModel(AppDbContext db, AuditService audit)
    {
        _db = db; _audit = audit;
    }

    public IList<PurchaseRequest> Pending { get; set; } = new List<PurchaseRequest>();

    public void OnGet()
    {
        Pending = _db.PurchaseRequests.Where(p => p.Status == "Submitted").ToList();
    }

    public IActionResult OnPostApprove(int id)
    {
        var pr = _db.PurchaseRequests.Find(id);
        if (pr == null) return NotFound();

        pr.Status = "Approved";
        _db.Approvals.Add(new Approval {
            PurchaseRequestId = id,
            ApproverUpn = User.Identity?.Name ?? "unknown@local",
            Action = "Approved"
        });
        _db.SaveChanges();

        _audit.Log(User.Identity?.Name ?? "", "ApproveRequest", $"PR#{id}");
        return RedirectToPage();
    }

    public IActionResult OnPostReject(int id, string? comments)
    {
        var pr = _db.PurchaseRequests.Find(id);
        if (pr == null) return NotFound();

        pr.Status = "Rejected";
        _db.Approvals.Add(new Approval {
            PurchaseRequestId = id,
            ApproverUpn = User.Identity?.Name ?? "unknown@local",
            Action = "Rejected",
            Comments = comments
        });
        _db.SaveChanges();

        _audit.Log(User.Identity?.Name ?? "", "RejectRequest", $"PR#{id}");
        return RedirectToPage();
    }
}
