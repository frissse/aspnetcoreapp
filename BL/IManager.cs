using System.Collections;
using PM.BL.Domain;
using PM.DAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PM.BL;

public interface IManager
{
    Episode GetEpisode(int id);
    Episode GetEpisodeWithGuestsHostUser(int id);
    Guest GetGuest(int id);
    IdentityUser GetUser(string email);
    IEnumerable<Episode> GetEpisodes();
    IEnumerable<Guest> GetGuests();
    IEnumerable<Guest> GetGuestsByGender(int input);
    IEnumerable<Episode> GetEpisodesByListenersCategory(int? listeners, int? category);
    IEnumerable<Sponsor> GetSponsors();
    IEnumerable<Host> GetHosts();
    Host GetHost(int id);
    Sponsor GetSponsor(int id);
    Episode AddEpisode(string title, TimeSpan duration, int episodeNumber, Category category, int listeners);
    Episode AddEpisodewithUser(string title, TimeSpan duration, int episodeNumber, Category category, int listeners, IdentityUser user);
    Guest AddGuest(string name, string expertise, Gender gender);
    Host AddHost(string name, int yearFirstPublished, double rating, Gender gender);
    IEnumerable<Episode> GetEpisodesWithHostGuestsUsers();
    Episode GetEpisodeWithGuestsAndHosts(int id);
    IEnumerable<Guest> GetGuestsWithEpisodes();
    Guest GetGuestWithEpisodes(int id);
    IEnumerable<Episode> GetEpisodesNotOfGuest(int guestId);
    void CreateEpisodeParticipation(int episodeId, int guestId, int sponsorId);
    void DeleteEpisodeParticipation(int episodeId, int guestId);
    IEnumerable<Episode> GetEpisodesOfGuests(int guestId);
    EpisodeParticipation GetEpisodeParticipation(int id);
    EpisodeParticipation CreateEpisodeParticipationWithSponsor(int episodeId, int guestId, DateTime dateRecorded, int? sponsorId);
    Episode ChangeEpisode(int id, int listeners);
}