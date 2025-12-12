using Microsoft.EntityFrameworkCore;
using ProcureDemo.Data;
using ProcureDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Auth (Azure AD)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opts =>
    {
        // For demo, you can switch to Cookie auth if AAD config isn't ready
        // Configure Authority, Audience per your Entra ID app registration
        // opts.Authority = "...";
        // opts.TokenValidationParameters.ValidAudience = "...";
    });

// For quick local demo, you can comment above and use:
builder.Services.AddAuthentication().AddCookie();

// Authorization policy
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("ApproverOnly", policy =>
        policy.RequireAssertion(ctx =>
            ctx.User.IsInRole("Approver") || ctx.User.IsInRole("Admin")));
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<AuditService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
