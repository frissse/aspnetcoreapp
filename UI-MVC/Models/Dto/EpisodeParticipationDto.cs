using System.ComponentModel.DataAnnotations;
using PM.BL.Domain;

namespace PM.UI.Web.MVC.Models.Dto;

public class EpisodeParticipationDto
{
    public int EpisodeId { get; set; }
    public int GuestId { get; set; }
    
    public string DateRecorded { get; set; }
    public int SponsorId { get; set; }
}