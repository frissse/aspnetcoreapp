using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM.BL.Domain;

namespace PM.DAL.EF;

public class Repository : IRepository
{
    private readonly PMdBContext _ctx; 
    public Repository(PMdBContext ctx)
    {
        _ctx = ctx;
    }

    public Episode ReadEpisode(int id)
    {
        return _ctx.Episodes.Include(e => e.User).SingleOrDefault(e => e.Id == id);
    }

    public Guest ReadGuest(int id)
    {
        return _ctx.Guests.Find(id);
    }

    public IdentityUser ReadUser(string email)
    {
        return _ctx.Users.Single(u => u.UserName == email);
    }

    public IEnumerable<Episode> ReadEpisodes()
    {
        return _ctx.Episodes;
    }

    public IEnumerable<Guest> ReadGuests()
    {
        return _ctx.Guests;
    }

    public Sponsor ReadSponsor(int? id)
    {
        return _ctx.Sponsors.Find(id);
    }
    
    public IEnumerable<Sponsor> ReadSponsors()
    {
        return _ctx.Sponsors;
    }
    
    public IEnumerable<Host> ReadHosts()
    {
        return _ctx.Hosts;
    }
    
    public Host ReadHost(int id)
    {
        return _ctx.Hosts.SingleOrDefault(h => h.Id == id);;
    }

    public IEnumerable<Guest> ReadGuestsByGender(int gender)
    {
        return _ctx.Guests.Where(g => g.Gender == (Gender)gender);
    }

    public IEnumerable<Episode> ReadEpisodesByHostRatingCategory(int? listeners, int? category)
    {
        return _ctx.Episodes.Include(e => e.Host).Include(e => e.GuestsOnEpisode).ThenInclude(e => e.Guest)
            .Where(e =>
            (listeners == null || e.Listeners >= listeners) && (category == null || e.Category == (Category)category));
    }

    public void CreateEpisode(Episode episode)
    {
        _ctx.Episodes.Add(episode);
        _ctx.SaveChanges();
    }

    public void CreateGuest(Guest guest)
    {
        _ctx.Guests.Add(guest);
        _ctx.SaveChanges();
    }
    
    public void CreateHost(Host host)
    {
        _ctx.Hosts.Add(host);
        _ctx.SaveChanges();
    }

    public IEnumerable<Episode> ReadAllEpisodesWithGuestsHostsAndUsers()
    {
        return _ctx.Episodes.Include(ep => ep.GuestsOnEpisode).ThenInclude(ep => ep.Guest)
            .Include(e => e.Host).Include(e => e.User);
    }

    public Episode ReadEpisodeWithGuestsHostUser(int id)
    {
        return _ctx.Episodes.Include(ep => ep.GuestsOnEpisode).ThenInclude(ep => ep.Guest)
            .Include(e => e.Host).Include(e => e.User).FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Guest> ReadAllGuestsWithEpisodes()
    {
        return _ctx.Guests.Include(ep => ep.EpisodeParticipations).ThenInclude(ep => ep.Episode);
    }

    public void CreateEpisodeParticipation(EpisodeParticipation episodeParticipation)
    {
        _ctx.EpisodeParticipations.Add(episodeParticipation);
        _ctx.SaveChanges();
    }

    public void DeleteEpisodeParticipation(int episodeId, int guestId)
    {
        var ep = _ctx.EpisodeParticipations.FirstOrDefault(ep => ep.Episode.Id == episodeId && ep.Guest.Id == guestId);
        _ctx.EpisodeParticipations.Remove(ep);
        _ctx.SaveChanges();
    }

    public IEnumerable<Episode> ReadEpisodesOfGuests(int guestId)
    {
        return _ctx.Episodes.Include(e => e.GuestsOnEpisode).ThenInclude(ep => ep.Guest)
            .Where(e => e.GuestsOnEpisode.Any(g => g.Guest.Id == guestId));
    }

    public Sponsor ReadSponsorOfEpisode(int episodeId)
    {
        return _ctx.EpisodeParticipations.Include(ep => ep.Sponsor).FirstOrDefault(ep => ep.Episode.Id == episodeId)?.Sponsor;
    }
    
    public Episode ReadEpisodeWithGuestsAndHosts(int id)
    {
        return _ctx.Episodes.Include(ep => ep.GuestsOnEpisode).ThenInclude(ep => ep.Guest).Include(e => e.Host)
            .FirstOrDefault(e => e.Id == id);
    }
    
    public Guest ReadEpisodesOfGuest(int id)
    {
        return _ctx.Guests.Include(ep => ep.EpisodeParticipations).ThenInclude(ep => ep.Episode)
            .FirstOrDefault(e => e.Id == id);
    }
    
    public IEnumerable<Episode> ReadEpisodesNotOfGuest(int guestId)
    {
        return _ctx.Episodes.Include(e => e.GuestsOnEpisode).ThenInclude(ep => ep.Guest)
            .Where(e => e.GuestsOnEpisode.All(g => g.Guest.Id != guestId));
    }
    
    public EpisodeParticipation ReadEpisodeParticipation(int id)
    {
        return _ctx.EpisodeParticipations.Find(id);
    }

    public Episode UpdateEpisode(Episode episode)
    {
        _ctx.Episodes.Update(episode);
        _ctx.SaveChanges();
        return episode;
    }
}