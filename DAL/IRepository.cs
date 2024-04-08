using Microsoft.AspNetCore.Identity;
using PM.BL.Domain;

namespace PM.DAL;

public interface IRepository
{
    Episode ReadEpisode(int id);
    Guest ReadGuest(int id);
    IdentityUser ReadUser(string email);
    IEnumerable<Episode> ReadEpisodes();
    IEnumerable<Guest> ReadGuests();
    IEnumerable<Guest> ReadGuestsByGender(int gender);
    IEnumerable<Episode> ReadEpisodesByHostRatingCategory(int? listeners, int? category);
    Sponsor ReadSponsor(int? id);
    IEnumerable<Sponsor> ReadSponsors();
    IEnumerable<Host> ReadHosts();
    Host ReadHost(int id);
    void CreateEpisode(Episode episode);
    void CreateGuest(Guest guest);
    void CreateHost(Host host);
    IEnumerable<Episode> ReadAllEpisodesWithGuestsHostsAndUsers();
    Episode ReadEpisodeWithGuestsAndHosts(int id);
    Episode ReadEpisodeWithGuestsHostUser(int id);
    IEnumerable<Guest> ReadAllGuestsWithEpisodes();
    Sponsor ReadSponsorOfEpisode(int episodeId);
    Guest ReadEpisodesOfGuest(int episodeId);
    IEnumerable<Episode> ReadEpisodesNotOfGuest(int guestId);
    void CreateEpisodeParticipation(EpisodeParticipation episodeParticipation);
    void DeleteEpisodeParticipation(int episodeId, int guestId);
    IEnumerable<Episode> ReadEpisodesOfGuests(int guestId);
    EpisodeParticipation ReadEpisodeParticipation(int id);
    Episode UpdateEpisode(Episode episode);
}

