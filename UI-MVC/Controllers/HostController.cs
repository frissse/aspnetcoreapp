using Microsoft.AspNetCore.Mvc;

namespace PM.UI.Web.MVC.Controllers;

public class HostController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}