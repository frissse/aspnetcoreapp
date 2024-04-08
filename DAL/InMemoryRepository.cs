using Microsoft.AspNetCore.Identity;
using PM.BL.Domain;

namespace PM.DAL;

public class InMemoryRepository : IRepository
{
    private static ICollection<Episode> _episodes = new List<Episode>();
    private static ICollection<Guest> _guests = new List<Guest>();

    public static void Seed()
    {
        var elonMusk = new Guest("Elon Musk", "business", Gender.Male);
        var davidSachs = new Guest("David Sachs", "making a shit ton of money", Gender.Male);
        var walterIsaacson = new Guest("Walter Isaacson", "biographies", Gender.Male);
        var clementineJacobs = new Guest("Clementine Jacobs", "statistics", Gender.Female);
        var christineEmbda = new Guest("Christine Emba", "feminism", Gender.Female);
        var taylorOliver = new Guest("Taylor Oliver", "productivity", Gender.Female);
        var christianRamsey = new Guest("Christian Ramsey", "stock market", Gender.Male);
        var brandonTaylor = new Guest("Brandon Taylor", "tech", Gender.Male);

        var joeRogan = new Host(1, "Joe Rogan", 2009, 4.5, Gender.Male);
        var jasonCalacanis = new Host(10, "Jason Calacanis", 2007, 4.8, Gender.Male);
        var lexFriedman = new Host(2, "Lex Friedman", 2006, 4.7, Gender.Male);
        var steveLevitt = new Host(3, "Steve Levitt", 2019, 3.9, Gender.Male);
        var liamJohnson = new Host(4, "Liam Johnson", 2009, 4.5, Gender.Male);
        var ethanSmith = new Host(5, "Ethan Smith", 2005, 3.1, Gender.Male);
        var benjaminDavis = new Host(6, "Benjamin Davis", 2010, 4.5, Gender.Male);
        var oliviaWilliams = new Host(7, "Olivia Williams", 2023, 2.5, Gender.Female);
        var emmaBrown = new Host(8, "Emma Brown", 2010, 3.2, Gender.Female);
        var sophiaTaylor = new Host(9, "Sophia Taylor", 2017, 2.7, Gender.Female);

        var joeRogan123 = new Episode("The Joe Rogan Podcast #123 with Elon Musk", new TimeSpan(0, 3, 43, 20),
            Category.Technology, 123, 874320);
        var allInElonMusk = new Episode("The All-in Podcast with Elon Musk", new TimeSpan(0, 3, 43, 20), Category.Business, 487,
            629171);
        var lexFriedmanWalterIscaacson = new Episode("The Lex Fridman Pdocast with Walter Isaacson", new TimeSpan(0, 2, 34, 45),
            Category.Culture, 243, 147635);
        var dataPrison = new Episode("Can Data Keep you out of Prison?", new TimeSpan(0, 51, 0), Category.Culture, 1050,
            765849);
        var feministMasculinity = new Episode("Talking To A Feminist about Masculinity", new TimeSpan(0, 45, 46), Category.Culture, 664,
            398743);
        var techTalk = new Episode("Tech Talk: Unveiling the Latest Breakthroughs in Artificial Intelligence",
            new TimeSpan(1, 20, 20), Category.Technology, 43, 239875);
        var marketInsights = new Episode("Market Insights: Navigating the Volatility - A Deep Dive into Stock Market Trends",
            new TimeSpan(0, 45, 20), Category.Business, 147, 563498);
        var productivityHacks = new Episode("Productivity Hacks: Mastering Time Management for Peak Efficiency",
            new TimeSpan(1, 45, 0), Category.Productivity, 354, 918245);
        var AIRevolution = new Episode("AI Revolution: How Artificial Intelligence is Reshaping Industries Worldwide",
            new TimeSpan(2, 25, 0), Category.Technology, 354, 746832);
        var hoursDay = new Episode("How to get more hours into your day", new TimeSpan(0, 45, 0), Category.Productivity, 254,
            312567);
        
        joeRogan123.Host = joeRogan;
        allInElonMusk.Host = jasonCalacanis;
        lexFriedmanWalterIscaacson.Host = lexFriedman;
        dataPrison.Host = steveLevitt;
        feministMasculinity.Host = liamJohnson;
        techTalk.Host = benjaminDavis;
        marketInsights.Host = sophiaTaylor;
        productivityHacks.Host = emmaBrown;
        AIRevolution.Host = oliviaWilliams;
        hoursDay.Host = ethanSmith;

        var athleticGreens = new Sponsor("athleticGreens", "your healthy daily drink", 10000);
        var squarespace = new Sponsor("squarespace", "your one stop shop for all your website needs", 5000);
        var cashApp = new Sponsor("cashApp", "your one stop shop for all your financial needs", 10000);
        var expressVPN = new Sponsor("expressVPN", "your one stop shop for all your privacy needs", 10000);
        var betterHelp = new Sponsor("betterHelp", "your one stop shop for all your mental health needs", 10000);

        var ep1 = new EpisodeParticipation(joeRogan123, elonMusk, new DateTime(2021, 01, 20), athleticGreens);
        var ep2 = new EpisodeParticipation(allInElonMusk, elonMusk, new DateTime(2022, 01, 20), cashApp);
        var ep3 = new EpisodeParticipation(allInElonMusk, davidSachs, new DateTime(2022, 01, 21), expressVPN);
        var ep4 = new EpisodeParticipation(lexFriedmanWalterIscaacson, walterIsaacson, new DateTime(2023, 08, 20), expressVPN);
        var ep5 = new EpisodeParticipation(dataPrison, clementineJacobs, new DateTime(2023, 05, 23), betterHelp);
        var ep6 = new EpisodeParticipation(feministMasculinity, christineEmbda, new DateTime(2022, 09, 13), betterHelp);
        var ep7 = new EpisodeParticipation(techTalk, brandonTaylor, new DateTime(2023, 10, 23), squarespace);
        var ep8 = new EpisodeParticipation(marketInsights, christianRamsey, new DateTime(2023, 09, 30), cashApp);
        var ep9 = new EpisodeParticipation(marketInsights, davidSachs, new DateTime(2023, 09, 30), squarespace);
        var ep10 = new EpisodeParticipation(productivityHacks, taylorOliver, new DateTime(2023, 09, 15), betterHelp);
        var ep11 = new EpisodeParticipation(productivityHacks, clementineJacobs, new DateTime(2023, 09, 15), athleticGreens);
        var ep12 = new EpisodeParticipation(AIRevolution, brandonTaylor, new DateTime(2023, 08, 15), cashApp);
        var ep13 = new EpisodeParticipation(hoursDay, taylorOliver, new DateTime(2023, 08, 15), squarespace);

        joeRogan123.GuestsOnEpisode = new List<EpisodeParticipation>() { ep1 };
        allInElonMusk.GuestsOnEpisode = new List<EpisodeParticipation>() { ep2, ep3 };
        lexFriedmanWalterIscaacson.GuestsOnEpisode = new List<EpisodeParticipation>() { ep4 };
        dataPrison.GuestsOnEpisode = new List<EpisodeParticipation>() { ep5 };
        feministMasculinity.GuestsOnEpisode = new List<EpisodeParticipation>() { ep6 };
        techTalk.GuestsOnEpisode = new List<EpisodeParticipation>() { ep7 };
        marketInsights.GuestsOnEpisode = new List<EpisodeParticipation>() { ep8, ep9 };
        productivityHacks.GuestsOnEpisode = new List<EpisodeParticipation>() { ep10, ep11 };
        AIRevolution.GuestsOnEpisode = new List<EpisodeParticipation>() { ep12 };
        hoursDay.GuestsOnEpisode = new List<EpisodeParticipation>() { ep13 };

        athleticGreens.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep1, ep12 };
        squarespace.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep7, ep9, ep13 };
        cashApp.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep2, ep8, ep12 };
        expressVPN.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep3, ep4 };
        betterHelp.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep5, ep6, ep10};
        
        
    }
    
    public Episode ReadEpisode(int id)
    {
        return _episodes.FirstOrDefault(item => item.Id == id);
    }

    public Guest ReadGuest(int id)
    {
        return _guests.FirstOrDefault(item => item.Id == id);
    }

    public IdentityUser ReadUser(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Episode> ReadEpisodes()
    {
        return _episodes;
        
    }

    public IEnumerable<Guest> ReadGuests()
    {
        return _guests;
        
    }

    public IEnumerable<Guest> ReadGuestsByGender(int input)
    {
        var inputByte = (byte)input;
        var gender = (Gender)inputByte;
        return _guests.Where(r => r.Gender == gender);
    }

    public IEnumerable<Episode> ReadEpisodesByHostRatingCategory(int? listeners, int? category)
    {
        return _episodes.Where(e => (listeners == null || e.Listeners == listeners) && (category == null || e.Category == (Category) category));
    }


    public void CreateEpisode(Episode episode)
    {
        if (_episodes == null || _episodes.Count == 0)
        {
            episode.Id = 1;
        }
        else
        {
            episode.Id = _episodes.Max(t => t.Id) + 1;
        }
        
        _episodes.Add(episode);
    }

    public void CreateGuest(Guest guest)
    {
        if (_guests == null || _guests.Count == 0)
        {
            guest.Id = 1;
        }
        else
        {
            guest.Id = _guests.Max(t => t.Id) + 1;
        }
        
        _guests.Add(guest);
    }
    
    public IEnumerable<Episode> ReadAllEpisodesWithGuestsHostsAndUsers()
    {
        throw new NotImplementedException();
    }

    public Episode ReadEpisodeWithGuestsHostUser(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Guest> ReadAllGuestsWithEpisodes()
    {
        throw new NotImplementedException();
    }
    
    public void CreateEpisodeParticipation(EpisodeParticipation episodeParticipation)
    {
        throw new NotImplementedException();
    }
    
    public void DeleteEpisodeParticipation(int episodeId, int guestId)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Episode> ReadEpisodesOfGuests(int guestId)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Sponsor> ReadSponsors()
    {
        throw new NotImplementedException();
    }
    
    public Sponsor ReadSponsor(int? id)
    {
        throw new NotImplementedException();
    }

    public Sponsor ReadSponsorOfEpisode(int episodeId)
    {
        throw new NotImplementedException();
    }
    
    public Episode ReadEpisodeWithGuestsAndHosts(int id)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Host> ReadHosts()
    {
        throw new NotImplementedException();
    }
    
    public Host ReadHost(int id)
    {
        throw new NotImplementedException();
    }
    
    public void CreateHost(Host host)
    {
        throw new NotImplementedException();
    }
    
    public Guest ReadEpisodesOfGuest(int episodeId)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Episode> ReadEpisodesNotOfGuest(int guestId)
    {
        throw new NotImplementedException();
    }
    
    public EpisodeParticipation ReadEpisodeParticipation(int id)
    {
        throw new NotImplementedException();
    }

    public Episode UpdateEpisode(Episode episode)
    {
        throw new NotImplementedException();
    }
}