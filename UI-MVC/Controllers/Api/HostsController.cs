using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.BL;

namespace PM.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class HostsController : ControllerBase
{
    private readonly IManager _mgr;
    
    public HostsController(IManager manager)
    {
        _mgr = manager;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var hosts = _mgr.GetHosts();

        if (hosts == null || !hosts.Any())
            return NoContent();
        
        return Ok(hosts);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Add(PM.BL.Domain.Host host)
    {
        if (!User.Identity.IsAuthenticated)
            return Forbid();

        var h = _mgr.AddHost(host.Name, host.YearFirstPublished, host.Rating, host.Gender);
        return CreatedAtAction(nameof(Add), h);
    }
}