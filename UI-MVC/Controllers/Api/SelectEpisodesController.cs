using Microsoft.AspNetCore.Mvc;
using PM.BL;
using PM.BL.Domain;
using PM.UI.Web.MVC.Models.Dto;

namespace PM.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SelectEpisodesController : ControllerBase
{
    private readonly IManager _mgr;
    
    public SelectEpisodesController(IManager manager)
    {
        _mgr = manager;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var episodes = _mgr.GetEpisodesNotOfGuest(id);
        if (episodes == null)
            return NoContent();

        return Ok(episodes.Select(eps => new EpisodeDto
        {
            Id = eps.Id,
            EpisodeTitle = eps.EpisodeTitle,
        }));
    }
}