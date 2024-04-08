using Microsoft.AspNetCore.Identity;
using PM.BL.Domain;

namespace PM.DAL.EF;

public class DataSeeder
{
    public static void Seed(PMdBContext context)
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
        var lexFriedman = new Host(2, "Lex Friedman", 2006, 4.7, Gender.Male);
        var steveLevitt = new Host(3, "Steve Levitt", 2019, 3.9, Gender.Male);
        var liamJohnson = new Host(4, "Liam Johnson", 2009, 4.5, Gender.Male);
        var ethanSmith = new Host(5, "Ethan Smith", 2005, 3.1, Gender.Male);
        var benjaminDavis = new Host(6, "Benjamin Davis", 2010, 4.5, Gender.Male);
        var oliviaWilliams = new Host(7, "Olivia Williams", 2023, 2.5, Gender.Female);
        var emmaBrown = new Host(8, "Emma Brown", 2010, 3.2, Gender.Female);
        var sophiaTaylor = new Host(9, "Sophia Taylor", 2017, 2.7, Gender.Female);
        var jasonCalacanis = new Host(10, "Jason Calacanis", 2007, 4.8, Gender.Male);
        
        var user1 = context.Users.Single(u => u.Email == "user1@app.com" || u.UserName == "user1@app.com");
        var user2 = context.Users.Single(u => u.Email == "user2@app.com" || u.UserName == "user2@app.com");
        var user3 = context.Users.Single(u => u.Email == "user3@app.com" || u.UserName == "user3@app.com");
        var user4 = context.Users.Single(u => u.Email == "user4@app.com" || u.UserName == "user4@app.com");
        var user5 = context.Users.Single(u => u.Email == "user5@app.com" || u.UserName == "user5@app.com");
        

        var joeRogan123 = new Episode("The Joe Rogan Podcast #123 with Elon Musk", new TimeSpan(0, 3, 43, 20),
            Category.Technology, 123, 874320, user1);
        var allInElonMusk = new Episode("The All-in Podcast with Elon Musk", new TimeSpan(0, 3, 43, 20), Category.Business, 487,
            629171, user2);
        var lexFriedmanWalterIscaacson = new Episode("The Lex Fridman Pdocast with Walter Isaacson", new TimeSpan(0, 2, 34, 45),
            Category.Culture, 243, 147635, context.Users.Single(u => u.Email =="user2@app.com"));
        var dataPrison = new Episode("Can Data Keep you out of Prison?", new TimeSpan(0, 51, 0), Category.Culture, 1050,
            765849, user3);
        var feministMasculinity = new Episode("Talking To A Feminist about Masculinity", new TimeSpan(0, 45, 46), Category.Culture, 664,
            398743, user3);
        var techTalk = new Episode("Tech Talk: Unveiling the Latest Breakthroughs in Artificial Intelligence",
            new TimeSpan(1, 20, 20), Category.Technology, 43, 239875, user2);
        var marketInsights = new Episode("Market Insights: Navigating the Volatility - A Deep Dive into Stock Market Trends",
            new TimeSpan(0, 45, 20), Category.Business, 147, 563498, user4);
        var productivityHacks = new Episode("Productivity Hacks: Mastering Time Management for Peak Efficiency",
            new TimeSpan(1, 45, 0), Category.Productivity, 354, 918245, user4);
        var aiRevolution = new Episode("AI Revolution: How Artificial Intelligence is Reshaping Industries Worldwide",
            new TimeSpan(2, 25, 0), Category.Technology, 354, 746832, user5);
        var hoursDay = new Episode("How to get more hours into your day", null, Category.Productivity, 254,
            312567, user4);
        
        joeRogan123.Host = joeRogan;
        allInElonMusk.Host = jasonCalacanis;
        lexFriedmanWalterIscaacson.Host = lexFriedman;
        dataPrison.Host = steveLevitt;
        feministMasculinity.Host = liamJohnson;
        techTalk.Host = benjaminDavis;
        marketInsights.Host = sophiaTaylor;
        productivityHacks.Host = emmaBrown;
        aiRevolution.Host = oliviaWilliams;
        hoursDay.Host = ethanSmith;

        var athleticGreens = new Sponsor("athleticGreens", "your healthy daily drink", 10000);
        var squarespace = new Sponsor("squarespace", "your one stop shop for all your website needs", 5000);
        var cashApp = new Sponsor("cashApp", "your one stop shop for all your financial needs", 10000);
        var expressVpn = new Sponsor("expressVPN", "your one stop shop for all your privacy needs", 10000);
        var betterHelp = new Sponsor("betterHelp", "your one stop shop for all your mental health needs", 10000);

        var ep1 = new EpisodeParticipation(joeRogan123, elonMusk, new DateTime(2021, 01, 20), athleticGreens);
        var ep2 = new EpisodeParticipation(allInElonMusk, elonMusk, new DateTime(2022, 01, 20), cashApp);
        var ep3 = new EpisodeParticipation(allInElonMusk, davidSachs, new DateTime(2022, 01, 21), expressVpn);
        var ep4 = new EpisodeParticipation(lexFriedmanWalterIscaacson, walterIsaacson, new DateTime(2023, 08, 20), expressVpn);
        var ep5 = new EpisodeParticipation(dataPrison, clementineJacobs, new DateTime(2023, 05, 23), betterHelp);
        var ep6 = new EpisodeParticipation(feministMasculinity, christineEmbda, new DateTime(2022, 09, 13), betterHelp);
        var ep7 = new EpisodeParticipation(techTalk, brandonTaylor, new DateTime(2023, 10, 23), squarespace);
        var ep8 = new EpisodeParticipation(marketInsights, christianRamsey, new DateTime(2023, 09, 30), cashApp);
        var ep9 = new EpisodeParticipation(marketInsights, davidSachs, new DateTime(2023, 09, 30), squarespace);
        var ep10 = new EpisodeParticipation(productivityHacks, taylorOliver, new DateTime(2023, 09, 15), betterHelp);
        var ep11 = new EpisodeParticipation(productivityHacks, clementineJacobs, new DateTime(2023, 09, 15), athleticGreens);
        var ep12 = new EpisodeParticipation(aiRevolution, brandonTaylor, new DateTime(2023, 08, 15), cashApp);
        var ep13 = new EpisodeParticipation(hoursDay, taylorOliver, new DateTime(2023, 08, 15), squarespace);

        joeRogan123.GuestsOnEpisode = new List<EpisodeParticipation>() { ep1 };
        allInElonMusk.GuestsOnEpisode = new List<EpisodeParticipation>() { ep2, ep3 };
        lexFriedmanWalterIscaacson.GuestsOnEpisode = new List<EpisodeParticipation>() { ep4 };
        dataPrison.GuestsOnEpisode = new List<EpisodeParticipation>() { ep5 };
        feministMasculinity.GuestsOnEpisode = new List<EpisodeParticipation>() { ep6 };
        techTalk.GuestsOnEpisode = new List<EpisodeParticipation>() { ep7 };
        marketInsights.GuestsOnEpisode = new List<EpisodeParticipation>() { ep8, ep9 };
        productivityHacks.GuestsOnEpisode = new List<EpisodeParticipation>() { ep10, ep11 };
        aiRevolution.GuestsOnEpisode = new List<EpisodeParticipation>() { ep12 };
        hoursDay.GuestsOnEpisode = new List<EpisodeParticipation>() { ep13 };
        
        athleticGreens.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep1, ep12 };
        squarespace.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep7, ep9, ep13 };
        cashApp.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep2, ep8, ep12 };
        expressVpn.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep3, ep4 };
        betterHelp.MentionedOnEpisodes = new List<EpisodeParticipation>() { ep5, ep6, ep10};

        context.Guests.Add(elonMusk);
        context.Guests.Add(davidSachs);
        context.Guests.Add(walterIsaacson);
        context.Guests.Add(clementineJacobs);
        context.Guests.Add(christineEmbda);
        context.Guests.Add(taylorOliver);
        context.Guests.Add(christianRamsey);
        context.Guests.Add(brandonTaylor);

        context.Episodes.Add(joeRogan123);
        context.Episodes.Add(allInElonMusk);
        context.Episodes.Add(lexFriedmanWalterIscaacson);
        context.Episodes.Add(dataPrison);
        context.Episodes.Add(feministMasculinity);
        context.Episodes.Add(techTalk);
        context.Episodes.Add(marketInsights);
        context.Episodes.Add(productivityHacks);
        context.Episodes.Add(aiRevolution);
        context.Episodes.Add(hoursDay);

        context.Hosts.Add(joeRogan);
        context.Hosts.Add(jasonCalacanis);
        context.Hosts.Add(lexFriedman);
        context.Hosts.Add(steveLevitt);
        context.Hosts.Add(liamJohnson);
        context.Hosts.Add(benjaminDavis);
        context.Hosts.Add(sophiaTaylor);
        context.Hosts.Add(emmaBrown);
        context.Hosts.Add(oliviaWilliams);
        context.Hosts.Add(ethanSmith);
        
        context.EpisodeParticipations.Add(ep1);
        context.EpisodeParticipations.Add(ep2);
        context.EpisodeParticipations.Add(ep3);
        context.EpisodeParticipations.Add(ep4);
        context.EpisodeParticipations.Add(ep5);
        context.EpisodeParticipations.Add(ep6);
        context.EpisodeParticipations.Add(ep7);
        context.EpisodeParticipations.Add(ep8);
        context.EpisodeParticipations.Add(ep9);
        context.EpisodeParticipations.Add(ep10);
        context.EpisodeParticipations.Add(ep11);
        context.EpisodeParticipations.Add(ep12);
        context.EpisodeParticipations.Add(ep13);
        
        context.Sponsors.Add(athleticGreens);
        context.Sponsors.Add(squarespace);
        context.Sponsors.Add(cashApp);
        context.Sponsors.Add(expressVpn);
        context.Sponsors.Add(betterHelp);

        context.SaveChanges();

        context.ChangeTracker.Clear();
    }
}