using PM.BL.Domain;

namespace PM.UI.CA.Extensions;

public static class GuestExtensions
{
    public static string PrintGuest(this Guest guest)
    {
        var ids = "no episodes yet";
        if (guest.EpisodeParticipations.Count > 1)
        {
            ids = string.Join(", ", guest.EpisodeParticipations.Select(ep => ep.Episode.EpisodeTitle));
            return $"Guest: {guest.Name}, appeared in episodes # {ids}, expertise: {guest.Expertise}, gender: {guest.Gender}";
        }
        
        return $"Guest: {guest.Name}, appeared in {ids}, expertise: {guest.Expertise}, gender: {guest.Gender}";
        
    }
}