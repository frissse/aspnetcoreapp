using System.ComponentModel.DataAnnotations;
using PM.BL.Domain;

namespace PM.UI.Web.MVC.Models.Dto;

public class EpisodeDto : IValidatableObject
{
    public int Id { get; set; }
    [ Required ]
    [ StringLength(200) ]
    public string EpisodeTitle { get; set; }
    public TimeSpan? Duration { get; set; }
    [ Range(1, 1000)]
    public int EpisodeNumber { get; set; }
    public string Category { get; set; }
    public int Listeners { get; set; }
    
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