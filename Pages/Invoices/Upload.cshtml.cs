using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProcureDemo.Data;
using ProcureDemo.Models;
using ProcureDemo.Services;

public class UploadModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly StorageService _storage;
    private readonly AuditService _audit;

    public UploadModel(AppDbContext db, StorageService storage, AuditService audit)
    {
        _db = db; _storage = storage; _audit = audit;
    }

    [BindProperty] public int PurchaseRequestId { get; set; }
    [BindProperty] public decimal Amount { get; set; }
    [BindProperty] public IFormFile? File { get; set; }

    public void OnGet() {}

    public async Task<IActionResult> OnPostAsync()
    {
        if (File == null || File.Length == 0) return Page();
        var webPath = await _storage.SaveInvoiceAsync(File);

        var invoice = new Invoice {
            PurchaseRequestId = PurchaseRequestId,
            Amount = Amount,
            FileName = File.FileName,
            StoragePath = webPath,
            UploadedByUpn = User.Identity?.Name ?? "unknown@local"
        };
        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();

        _audit.Log(invoice.UploadedByUpn, "UploadInvoice", $"PR#{PurchaseRequestId} | {File.FileName}");
        return RedirectToPage("Index");
    }
}
