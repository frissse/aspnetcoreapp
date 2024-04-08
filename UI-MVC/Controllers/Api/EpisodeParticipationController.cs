using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.BL;
using PM.UI.Web.MVC.Models.Dto;

namespace PM.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class EpisodeParticipationController : ControllerBase
{
    private readonly IManager _mgr;
    
    public EpisodeParticipationController(IManager manager)
    {
        _mgr = manager;
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var episodeParticipation = _mgr.GetEpisodeParticipation(id);

        if (episodeParticipation == null )
            return NoContent();
        
        return Ok(episodeParticipation);
    }
    
    [Authorize]
    [HttpPost]
    public ActionResult<AddEpisodeParticipationDto> Post(EpisodeParticipationDto eps)
    {
        var e = _mgr.CreateEpisodeParticipationWithSponsor(eps.EpisodeId, eps.GuestId, DateTime.Parse(eps.DateRecorded), eps.SponsorId);
        
        AddEpisodeParticipationDto addedEpisodeParticipation = new AddEpisodeParticipationDto
        {
            Id = e.Id,
            DateRecorded = e.DateRecorded.ToString("MM/dd/yyyy"),
            SponsorName = e.Sponsor.SponsorName
        };
        
        return CreatedAtAction("Get", new { id = addedEpisodeParticipation.Id }, addedEpisodeParticipation);
        
    }
}