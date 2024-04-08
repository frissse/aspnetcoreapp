using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM.BL;
using PM.BL.Domain;
using PM.UI.Web.MVC.Models;
using Host = PM.BL.Domain.Host;

namespace PM.UI.Web.MVC.Controllers;

public class EpisodeController : Controller
{
    private readonly IManager _mgr;
    private readonly UserManager<IdentityUser> _userManager;
    
    public EpisodeController(IManager mgr, UserManager<IdentityUser> usrmgr)
    {
        _mgr = mgr;
        _userManager = usrmgr;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Episode> episodes = _mgr.GetEpisodes();
        return View(episodes);
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Add(Episode episode)
    {
        if (!ModelState.IsValid)
        {
            return View(episode);
        }

        var userName = _userManager.GetUserName(User);
        IdentityUser user = _mgr.GetUser(userName);
        
        Episode e = _mgr.AddEpisodewithUser(episode.EpisodeTitle, episode.Duration.GetValueOrDefault(), episode.EpisodeNumber, episode.Category, episode.Listeners, user);
        return RedirectToAction("Details", "Episode", new { Id = e.Id });
    }
    
    [HttpGet]
    public IActionResult Details(int id)
    {
        Episode episode = _mgr.GetEpisodeWithGuestsHostUser(id);
        
        if (episode == null)
        {
            return NotFound();
        }
        
        return View(episode);
    }
    
    
}