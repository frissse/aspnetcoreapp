using PM.BL.Domain;
using PM.DAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PM.BL;

public class Manager : IManager
{
    private readonly IRepository _repository;
    
    public Manager(IRepository repository)
    {
        _repository = repository;
    }
    
    public Episode GetEpisode(int id)
    {
        return _repository.ReadEpisode(id);
    }

    public Episode GetEpisodeWithGuestsHostUser(int id)
    {
        return _repository.ReadEpisodeWithGuestsHostUser(id);
    }

    public Guest GetGuest(int id)
    {
        return _repository.ReadGuest(id);
    }

    public IdentityUser GetUser(string email)
    {
        return _repository.ReadUser(email);
    }

    public IEnumerable<Episode> GetEpisodes()
    {
        return _repository.ReadEpisodes();
    }

    public IEnumerable<Guest> GetGuests()
    {
        return _repository.ReadGuests();
    }
    
    public IEnumerable<Host> GetHosts()
    {
        return _repository.ReadHosts();
    }
    
    public Host GetHost(int id)
    {
        return _repository.ReadHost(id);
    }
    
    public Sponsor GetSponsor(int id)
    {
        return _repository.ReadSponsor(id);
    }
    
    public IEnumerable<Sponsor> GetSponsors()
    {
        return _repository.ReadSponsors();
    }

    public IEnumerable<Guest> GetGuestsByGender(int input)
    {
        return _repository.ReadGuestsByGender(input);
    }

    public IEnumerable<Episode> GetEpisodesByListenersCategory(int? listeners, int? category)
    {
        var result = _repository.ReadEpisodesByHostRatingCategory(listeners, category);
        if (result == null)
        {
            throw new ValidationException("No episodes found");
        }
        return result;
    }

    public Episode AddEpisode(string title, TimeSpan duration, int episodeNumber, Category category, int listeners)
    {
        var e = new Episode(title, duration,category,episodeNumber, listeners);
        ICollection<ValidationResult> result = new List<ValidationResult>();
        var context = new ValidationContext(e, null, null);
        bool isValid = Validator.TryValidateObject(e, context, result, validateAllProperties:true);

        if (!isValid)
        {
            string s = "";
            foreach (var r in result)
            {
                s += r.ErrorMessage + "\n";
            }
            throw new ValidationException(s);
        }
        
        _repository.CreateEpisode(e);

        return e;
    }

    public Episode AddEpisodewithUser(string title, TimeSpan duration, int episodeNumber, Category category, int listeners, IdentityUser user)
    {
        var e = new Episode(title, duration,category,episodeNumber, listeners, user);
        ICollection<ValidationResult> result = new List<ValidationResult>();
        var context = new ValidationContext(e, null, null);
        bool isValid = Validator.TryValidateObject(e, context, result, validateAllProperties:true);

        if (!isValid)
        {
            string s = "";
            foreach (var r in result)
            {
                s += r.ErrorMessage + "\n";
            }
            throw new ValidationException(s);
        }
        
        _repository.CreateEpisode(e);

        return e;
    }


    public Guest AddGuest(string name, string expertise, Gender gender)
    {
        var g = new Guest(name, expertise, gender);
        ICollection<ValidationResult> result = new List<ValidationResult>();
        var context = new ValidationContext(g, null, null);
        bool isValid = Validator.TryValidateObject(g, context, result, validateAllProperties:true);
        
        if (!isValid)
        {
            string s = "";
            foreach (var r in result)
            {
                s += r.ErrorMessage + "\n";
            }
            throw new ValidationException(s);
        }
        
        _repository.CreateGuest(g);

        return g;
    }

    public Host AddHost(string name, int yearFirstPublished, double rating, Gender gender)
    {
        var h = new PM.BL.Domain.Host(name, yearFirstPublished, rating, gender);
        _repository.CreateHost(h);
        return h;
    }

    public IEnumerable<Episode> GetEpisodesWithHostGuestsUsers()
    {
        return _repository.ReadAllEpisodesWithGuestsHostsAndUsers();
    }
    
    public IEnumerable<Guest> GetGuestsWithEpisodes()
    {
        return _repository.ReadAllGuestsWithEpisodes();
    }
    
    public void CreateEpisodeParticipation(int episodeId, int guestId, int sponsorId)
    {
        var e = _repository.ReadEpisode(episodeId);
        var g = _repository.ReadGuest(guestId);
        var s = _repository.ReadSponsor(sponsorId);
        var ep = new EpisodeParticipation(e, g, DateTime.Today, s);
        _repository.CreateEpisodeParticipation(ep);
    }
    
    public void DeleteEpisodeParticipation(int episodeId, int guestId)
    {
        _repository.DeleteEpisodeParticipation(episodeId, guestId);
    }
    
    public IEnumerable<Episode> GetEpisodesOfGuests(int guestId)
    {
        return _repository.ReadEpisodesOfGuests(guestId);
    }

    public Sponsor GetSponsorOfEpisode(int sponsorId)
    {
        return _repository.ReadSponsorOfEpisode(sponsorId);
    }
    
    public Episode GetEpisodeWithGuestsAndHosts(int id)
    {
        return _repository.ReadEpisodeWithGuestsAndHosts(id);
    }
    
    public Guest GetGuestWithEpisodes(int id)
    {
        return _repository.ReadEpisodesOfGuest(id);
    }
    
    public IEnumerable<Episode> GetEpisodesNotOfGuest(int guestId)
    {
        return _repository.ReadEpisodesNotOfGuest(guestId);
    }
    
    public EpisodeParticipation CreateEpisodeParticipationWithSponsor(int episodeId, int guestId, DateTime dateRecorded, int? sponsorId)
    {
        var e = _repository.ReadEpisode(episodeId);
        var g = _repository.ReadGuest(guestId);
        var s = _repository.ReadSponsor(sponsorId);
        var ep = new EpisodeParticipation(e, g, dateRecorded, s);
        _repository.CreateEpisodeParticipation(ep);
        return ep;
    }

    public Episode ChangeEpisode(int id, int listeners)
    {
        var episode = _repository.ReadEpisode(id);
        episode.Listeners = listeners;
        _repository.UpdateEpisode(episode);
        return episode;
    }

    public EpisodeParticipation GetEpisodeParticipation(int id)
    {
        return _repository.ReadEpisodeParticipation(id);
    }
}