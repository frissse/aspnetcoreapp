using System.ComponentModel.DataAnnotations;

namespace PM.BL.Domain;

public class Sponsor
{   
    [Key]
    public int Id { get; set; }
    public String SponsorName { get; set; }
    public String SponsorDescription { get; set; }
    public float SponsorValue { get; set; }
    public ICollection<EpisodeParticipation> MentionedOnEpisodes { get; set; }
    
    public Sponsor(string sponsorName, string sponsorDescription, float sponsorValue)
    {
        SponsorName = sponsorName;
        SponsorDescription = sponsorDescription;
        SponsorValue = sponsorValue;
        MentionedOnEpisodes = new List<EpisodeParticipation>();
    }
}