using Microsoft.AspNetCore.Mvc;
using PM.BL;
using PM.UI.Web.MVC.Models.Dto;


namespace PM.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class GuestsController : ControllerBase
{
    private readonly IManager _mgr;
    
    public GuestsController(IManager manager)
    {
        _mgr = manager;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var guest = _mgr.GetGuest(id);

        if (guest == null )
            return NoContent();
        
        return Ok(guest);
    }
    
    [HttpGet("{id}/episodes")]
    public IActionResult GetEpisodes(int id)
    {
        var episodes = _mgr.GetEpisodesOfGuests(id);

        if (episodes == null )
            return NoContent();
        
        return Ok(episodes.Select(e => new EpisodeDto
        {
            Id = e.Id,
            EpisodeTitle = e.EpisodeTitle,
            Duration = e.Duration,
            EpisodeNumber = e.EpisodeNumber,
            Category = e.Category.ToString(),
            Listeners = e.Listeners
        }));
    }
}