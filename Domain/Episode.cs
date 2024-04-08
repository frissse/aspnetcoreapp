using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PM.BL.Domain;

public class Episode : IValidatableObject
{
    [ Key ]
    public int Id { get; set; }
    [ Required ]
    [ StringLength(200) ]
    public string EpisodeTitle { get; set; }
    public TimeSpan? Duration { get; set; }
    [ Range(1, 1000)]
    public int EpisodeNumber { get; set; }
    public Category Category { get; set; }
    
    public Host Host { get; set; }
    public int Listeners { get; set; }
    public IdentityUser User { get; set; }
    public ICollection<EpisodeParticipation> GuestsOnEpisode { get; set; }
    
    public Episode(int id, string episodeTitle, TimeSpan? duration, int episodeNumber, Category category, List<Guest> guests, Host host, int listeners)
    {
        Id = id;
        EpisodeTitle = episodeTitle;
        Duration = duration;
        EpisodeNumber = episodeNumber;
        Category = category;
        Host = host;
        Listeners = listeners;
    }
    
    public Episode(string episodeTitle, TimeSpan? duration, int episodeNumber, Category category, List<Guest> guests, Host host, int listeners)
    {
        EpisodeTitle = episodeTitle;
        Duration = duration;
        EpisodeNumber = episodeNumber;
        Category = category;
        Host = host;
        Listeners = listeners;

    }

    public Episode(int id, string episodeTitle, TimeSpan? duration, Category category, int episodeNumber, int listeners, IdentityUser user)
    {
        Id = id;
        EpisodeTitle = episodeTitle;
        Duration = duration;
        Category = category;
        EpisodeNumber = episodeNumber;
        Host = null;
        Listeners = listeners;
        User = user;
    }
    public Episode(string episodeTitle, TimeSpan? duration, Category category, int episodeNumber, int listeners)
    {
        EpisodeTitle = episodeTitle;
        Duration = duration;
        Category = category;
        EpisodeNumber = episodeNumber;
        Host = null;
        Listeners = listeners;
    }

    public Episode(string title, TimeSpan? duration, Category category, int episodeNumber, int listeners, IdentityUser user)
    {
        EpisodeTitle = title;
        Duration = duration;
        Category = category;
        EpisodeNumber = episodeNumber;
        Host = null;
        Listeners = listeners;
        User = user;
    }
    
    public Episode(string title, TimeSpan? duration, Category category, int episodeNumber, int listeners, Host host)
    {
        EpisodeTitle = title;
        Duration = duration;
        Category = category;
        EpisodeNumber = episodeNumber;
        Host = host;
        Listeners = listeners;
    }

    public Episode()
    {
        
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        
        var minTimeSpan = new TimeSpan(0,1,0);
        var maxTimeSpan = new TimeSpan(4,0,10);
        
        if (minTimeSpan > Duration || maxTimeSpan < Duration)
        {
            errors.Add(new ValidationResult("An episode must be at least 10 minutes long and cannot be more than 4 hours", new string[] { "Duration" }));
        }

        return errors;
    }
}