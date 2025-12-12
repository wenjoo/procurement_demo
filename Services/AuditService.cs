using Microsoft.Extensions.Logging;

namespace ProcureDemo.Services
{
    public class AuditService
    {
        private readonly ILogger<AuditService> _logger;
        public AuditService(ILogger<AuditService> logger) => _logger = logger;

        public void Log(string upn, string action, string details)
        {
            _logger.LogInformation("Audit: {UPN} | {Action} | {Details} | {At}",
                upn, action, details, DateTime.UtcNow);
        }
    }
}
