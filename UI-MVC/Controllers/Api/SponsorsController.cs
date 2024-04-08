using Microsoft.AspNetCore.Mvc;
using PM.BL;
using PM.UI.Web.MVC.Models.Dto;

namespace PM.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SponsorsController : ControllerBase
{
    private readonly IManager _mgr;
    
    public SponsorsController(IManager manager)
    {
        _mgr = manager;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var sponsors = _mgr.GetSponsors();
        if (sponsors == null)
            return NoContent();
        return Ok(sponsors.Select(s => new SponsorDto
        {
            Id = s.Id,
            SponsorName = s.SponsorName
        }));
    }
}