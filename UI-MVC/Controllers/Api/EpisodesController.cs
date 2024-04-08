using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PM.BL;
using PM.UI.Web.MVC.Models.Dto;

namespace PM.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class EpisodesController : ControllerBase
{
    private readonly IManager _mgr;
    
    public EpisodesController(IManager manager)
    {
        _mgr = manager;
    }
    
    
    [HttpGet("{id}")]
    public IActionResult GetEpisode(int id)
    {
        var episode = _mgr.GetEpisode(id);
        
        if (episode == null)
            return NoContent();
        
        return Ok(episode);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateEpisode(int id, EditEpisodeDto episode)
    {
        var episodeToUpdate = _mgr.GetEpisode(id);
        
        if (episodeToUpdate == null)
            return NotFound();

        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        } 
        
        if (episodeToUpdate.User.UserName != User.Identity.Name && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        _mgr.ChangeEpisode(episodeToUpdate.Id, episode.Listeners);

        return NoContent();
    }
}