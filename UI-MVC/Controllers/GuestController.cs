using Microsoft.AspNetCore.Mvc;
using PM.BL;
using PM.BL.Domain;

namespace PM.UI.Web.MVC.Controllers;

public class GuestController : Controller
{
    private readonly IManager _mgr;
    
    public GuestController(IManager mgr)
    {
        _mgr = mgr;
    }
    
    // GET
    public IActionResult Details(int id)
    {
        Guest g = _mgr.GetGuest(id);
        
        if (g == null)
        {
            return NotFound();
        }
        
        return View(g);
    }
    
    
}