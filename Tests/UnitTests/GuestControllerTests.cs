using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;
using PM.UI.Web.MVC.Controllers;

namespace Tests.UnitTests;

public class GuestControllerTests
{
    [Fact]
    public void Details_GivenValidId_ShouldReturnDetailsView_WithGuestData()
    {
        int guestId = 1;
        var guest = new Guest("Elon Musk", "business", Gender.Male);
        var mockMgr = new Mock<IManager>();
        mockMgr.Setup(m => m.GetGuest(guestId))
            .Returns(guest);
        
        var controller = new GuestController(mockMgr.Object);
        
        var result = controller.Details(guestId);
        
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Details", viewResult.ViewName ?? nameof(GuestController.Details));
        mockMgr.VerifyAll();
    }
    
    [Fact]
    public void Details_GivenInvalidId_ShouldReturnNotFound()
    {
        int guestId = -1;
        var mockMgr = new Mock<IManager>();
        
        var controller = new GuestController(mockMgr.Object);
        
        var result = controller.Details(guestId);
        
        Assert.IsType<NotFoundResult>(result);
        mockMgr.VerifyAll();
    }
}