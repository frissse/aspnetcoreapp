using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PM.BL.Domain;

public class Guest
{
    public int Id { get; set; }
    [RegularExpression(@"^[A-Z][a-zA-Z]+\s[A-Z][a-zA-Z]+$")]
    public string Name { get; set; }
    [StringLength(100)]
    [RegularExpression(@"[a-zA-Z]+")]
    public string Expertise { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public ICollection<EpisodeParticipation> EpisodeParticipations { get; set; }

    public Guest(int id, string name, string expertise, Gender gender)
    {
        Id = id;
        Name = name;
        Expertise = expertise;
        Gender = gender;
        EpisodeParticipations = new List<EpisodeParticipation>();
    }
    
    public Guest(string name, string expertise, Gender gender)
    {
        Name = name;
        Expertise = expertise;
        Gender = gender;
        EpisodeParticipations = new List<EpisodeParticipation>();
    }
    
    public Guest(int id, string name, string expertise, Gender gender, List<EpisodeParticipation> episodesAppearedOn)
    {
        Id = id;
        Name = name;
        Expertise = expertise;
        Gender = gender;
        EpisodeParticipations = episodesAppearedOn;
    }
}