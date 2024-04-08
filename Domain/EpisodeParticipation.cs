using System.ComponentModel.DataAnnotations;

namespace PM.BL.Domain;

public class EpisodeParticipation
{
    [Key]
    public int Id { get; set; }
    [Required]
    public Episode Episode { get; set; }
    public Guest Guest { get; set; }
    public DateTime DateRecorded { get; set; }
    public Sponsor Sponsor { get; set; }

    public EpisodeParticipation(Episode episode, Guest guest, DateTime dateRecorded, Sponsor sponsor)
    {
        Episode = episode;
        Guest = guest;
        DateRecorded = dateRecorded;
        Sponsor = sponsor;
    }
    
    public EpisodeParticipation(Episode episode, Guest guest, DateTime dateRecorded)
    {
        Episode = episode;
        Guest = guest;
        DateRecorded = dateRecorded;
    }
    
    public EpisodeParticipation()
    {
        
    }
}