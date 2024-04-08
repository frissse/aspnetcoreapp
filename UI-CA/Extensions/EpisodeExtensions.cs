using System.Reflection.Metadata.Ecma335;
using PM.BL.Domain;

namespace PM.UI.CA.Extensions;

public static class EpisodeExtensions
{
    public static string PrintEpisode(this Episode episode)
    {
        var guests = "no guests added";
        if (episode.GuestsOnEpisode != null)
        {
            guests = string.Join(", ", episode.GuestsOnEpisode.Select(ep => ep.Guest.Name));     
        } 
        
        TimeSpan? d = episode.Duration ?? new TimeSpan();
        
        return $"#{episode.EpisodeNumber,-5:D} {episode.EpisodeTitle}, guests: {guests}, duration: {d ,5:c}, host: {episode.Host?.Name ?? "no host added"}, category: {episode.Category}, # of listeners: {episode.Listeners}";
    }


}