using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("IndexModel - OnGet accessed from {IP} ({UserAgent}",
            HttpContext.Connection.RemoteIpAddress,
            HttpContext.Request.Headers["User-Agent"]);
    }
}
