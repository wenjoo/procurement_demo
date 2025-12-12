using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProcureDemo.Data;
using ProcureDemo.Models;
using ProcureDemo.Services;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly AuditService _audit;

    public CreateModel(AppDbContext db, AuditService audit)
    {
        _db = db; _audit = audit;
    }

    [BindProperty] public PurchaseRequest Request { get; set; } = new();

    public void OnGet() {}

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        Request.CreatedByUpn = User.Identity?.Name ?? "unknown@local";
        _db.PurchaseRequests.Add(Request);
        _db.SaveChanges();

        _audit.Log(Request.CreatedByUpn, "SubmitRequest", $"PR#{Request.Id} - {Request.Title}");
        return RedirectToPage("Index");
    }
}
