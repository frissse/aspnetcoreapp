using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;
using PM.UI.CA.Extensions;

namespace PM.UI.CA;

class ConsoleUi
{
    private readonly IManager _manager;

    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }

    public void Run()
    {
        int choice = 0;
        
        while (choice > -1)
        {
            choice = startConsole();
            IEnumerable<Episode> resultEpisode = new List<Episode>();
            switch (choice)
            {
                case 0:
                    choice = -1;
                    Console.WriteLine("good bye :-D");
                    break;
                case 1:
                    GetGuests();
                    break;
                case 2:
                    GetGuestsByGender();
                    break;
                case 3:
                    GetEpisodes();
                    break;
                case 4:
                    GetEpisodesByListenersCategory();
                    break;
                case 5:
                    AddGuest();
                    break;
                case 6:
                    AddEpisode();
                    break;
                case 7:
                    CreateEpisodeParticiaption();
                    break;
                case 8:
                    DeleteEpisodeParticiaption();
                    break;
                default:
                    Console.WriteLine("Your choise was not valid, has to be between 0 and 8\n");
                    break;
            }
        }
    }

    private int startConsole()
    {
        Console.WriteLine("What do you want to do?");
        Console.WriteLine(("======================"));
        Console.WriteLine(
            "0) Quit\n1) Show all guests\n2) Show guests of gender\n3) Show all episodes\n4) Show episodes by # of listeners and/or category\n5) Add a new guest\n6) Add a new episode\n7) Add a guest to an episode\n8) Remove a guest from an episode\n");
        Console.Write("Choice (0-8):");
        
        int answer;
        bool success;
        string choiceText = Console.ReadLine();
        while (!Int32.TryParse(choiceText, out answer))
        { 
            Console.WriteLine("input not correct try again, should be a number from 0 to 8");
            choiceText = Console.ReadLine();
        }
        return answer;
    }

    private void AddEpisode()
    {
        bool validateOk;
        Episode episode = null;
        do
        {
            try
            {
                Console.WriteLine("Episode title: ");
                var title = Console.ReadLine();
                Console.WriteLine("Episode Duration: use following format HH:mm:ss");
                var duration = TimeSpan.Parse(Console.ReadLine());
                foreach (Category c in Enum.GetValues(typeof(Category)))
                {
                    Console.WriteLine($"{(int)c}: {c}");
                }
                Console.WriteLine("Episode category: ");
                var episodeCategory = (Category)Int32.Parse(Console.ReadLine());
                Console.WriteLine("Episode number: ");
                var episodeNumber = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Episode listeners: ");
                var listeners = Int32.Parse(Console.ReadLine());
                episode = _manager.AddEpisode(title, duration, episodeNumber, episodeCategory, listeners);
                validateOk = true;
            }
            catch (ValidationException v)
            {
                Console.WriteLine(v.Message);
                validateOk = false;
            }
        } while (!validateOk);


        Console.WriteLine("Episode werd toegevoegd: " + episode.PrintEpisode());
    }

    private void AddGuest()
    {
        bool validateOk = true;
        Guest guest = null;
        do
        {
            try
            {
                Console.WriteLine("Guest name: ");
                var name = Console.ReadLine();
                Console.WriteLine("Guest expertise: enter a subject");
                var expertise = Console.ReadLine();
                Console.WriteLine("Guest gender(1=Male, 2=Female: ");
                var inputGender = Console.ReadLine();
                int gender;
                while (!Int32.TryParse(inputGender, out gender))
                {
                    Console.WriteLine("your input was not correct, try again");
                }
                guest = _manager.AddGuest(name, expertise, (Gender)gender);
            }
            catch (ValidationException v)
            {
                Console.WriteLine(v.Message);
                validateOk = false;
            }
        } while (!validateOk);


        Console.WriteLine("Guest werd toegevoegd: " + guest.PrintGuest());
    }

    private void GetGuests()
    {
        IEnumerable<Guest> resultGuests = new List<Guest>();
        resultGuests = _manager.GetGuestsWithEpisodes();

        if (resultGuests != null)
        {
            foreach (var guest in resultGuests)
            {
                var stringRep = guest.PrintGuest();
                Console.WriteLine(stringRep);
            }
        }
        else
        {
            Console.WriteLine("No guests found");
        }
        
        
    }

    private void GetGuestsByGender()
    {
        IEnumerable<Guest> resultGuests = new List<Guest>();
        foreach (Gender g in Enum.GetValues(typeof(Gender)))
        {
            Console.WriteLine($"{(int)g}: {g}");
        }

        string inputGender = Console.ReadLine();
        int gender;
        while (!Int32.TryParse(inputGender, out gender))
        {
            Console.WriteLine("your input was not correct, try again");
        }

        resultGuests = _manager.GetGuestsByGender(gender);
        foreach (var guest in resultGuests)
        {
            var stringRep = guest.PrintGuest();
            Console.WriteLine(stringRep);
        }
    }

    private void GetEpisodes()
    {
        IEnumerable<Episode> resultEpisodes = new List<Episode>();
        resultEpisodes = _manager.GetEpisodesWithHostGuestsUsers();
        foreach (var episode in resultEpisodes)
        {
            var stringRep = episode.PrintEpisode();
            Console.WriteLine(stringRep);
        }
    }

    private void GetEpisodesByListenersCategory()
    {
        IEnumerable<Episode> resultEpisode = new List<Episode>();
        Console.WriteLine("Amount of listeners (or leave blank): ");
        string inputListeners = Console.ReadLine();

        int tempVal;
        int? listeners = Int32.TryParse(inputListeners, out tempVal) ? tempVal : null;

        foreach (Category c in Enum.GetValues(typeof(Category)))
        {
            Console.WriteLine($"{(int)c}: {c}");
        }

        Console.WriteLine($"Category (or leave blank) choose from the above:");
        string inputCategory = Console.ReadLine();

        int tempCat;
        int? category = Int32.TryParse(inputCategory, out tempCat) ? tempCat : null;

        resultEpisode = _manager.GetEpisodesByListenersCategory(listeners, category);

        if (!resultEpisode.Any())
        {
            Console.WriteLine("No episodes found");
        }
        else
        {
            var cParsed = category ?? 0; 
            Console.WriteLine($"All episodes with {listeners} listeners and {Enum.GetName(typeof(Category),cParsed)} as category");
            foreach (var episode in resultEpisode)
            {
                var stringRep = episode.PrintEpisode();
                Console.WriteLine(stringRep);
            }
        }
    }

    private void CreateEpisodeParticiaption()
    {
        var episodes = _manager.GetEpisodes();
        var guests = _manager.GetGuests();
        var sponsors = _manager.GetSponsors();
        
        foreach (var episode in episodes)
        {
            Console.WriteLine($"{episode.Id}: {episode.EpisodeTitle}");
        }
        Console.WriteLine("which episode do you want to add a guest to?");
        
        string inputEpisode = Console.ReadLine();
        
        int eps;
        
        while (!Int32.TryParse(inputEpisode, out eps))
        {
            Console.WriteLine("your input was not correct, try again");
        }

        bool foundEpisode = false;
        

        while (!foundEpisode)
        {
            try
            {   
                var episodeCheck = _manager.GetEpisode(eps);
                if (episodeCheck != null)
                {
                    foundEpisode = true;
                }
                else
                {
                    Console.WriteLine("episode not found, enter another episode id");
                    inputEpisode = Console.ReadLine();
                    while (!Int32.TryParse(inputEpisode, out eps))
                    {
                        Console.WriteLine("your input was not correct, try again");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("episode not found, enter another episode id");
                throw;
            }
        }
        
        
        foreach (var guest in guests)
        {
            Console.WriteLine($"{guest.Id}: {guest.Name}");
        }
        
        Console.WriteLine("which guest do you want to add to the episode?");
        
        string inputGuest = Console.ReadLine();
        
        int g;
        
        while (!Int32.TryParse(inputGuest, out g))
        {
            Console.WriteLine("your input was not correct, try again");
        }
        
        bool foundGuest = false;

        while (!foundGuest)
        {
            try
            {   
                var guestCheck = _manager.GetGuest(g);
                if (guestCheck != null)
                {
                    foundGuest = true;
                }
                else
                {
                    Console.WriteLine("guest not found, enter another guest id");
                    inputGuest = Console.ReadLine();
                    while (!Int32.TryParse(inputGuest, out g))
                    {
                        Console.WriteLine("your input was not correct, try again");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("guest not found, enter another guest id");
                throw;
            }
        }

        foreach (var sponsor in sponsors)
        {
            Console.WriteLine($"{sponsor.Id}: {sponsor.SponsorName}");
        }
        Console.WriteLine("which sponsor do you want to add to the episode?");
        string inputSponsor = Console.ReadLine();
        int s;
        
        while (!Int32.TryParse(inputSponsor, out s))
        {
            Console.WriteLine("your input was not correct, try again");
        }
        
        bool foundSponsor = false;

        while (!foundSponsor)
        {
            try
            {   
                var sponsorCheck = _manager.GetSponsor(s);
                if (sponsorCheck != null)
                {
                    foundSponsor = true;
                }
                else
                {
                    Console.WriteLine("sponsor not found, enter another sponsor id");
                    inputSponsor = Console.ReadLine();
                    while (!Int32.TryParse(inputSponsor, out s))
                    {
                        Console.WriteLine("your input was not correct, try again");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("sponsor not found, enter another sponsor id");
                throw;
            }
        }
        
        _manager.CreateEpisodeParticipation(eps, g, s);
        Console.WriteLine($"episode participation added with episode Id {eps}, guest Id {g} and Sponsor {s}");
    }
    
    private void DeleteEpisodeParticiaption()
    {
        var episodes = _manager.GetEpisodes();
        var guests = _manager.GetGuests();
        foreach (var guest in guests)
        {
            Console.WriteLine($"{guest.Id}: {guest.Name}");
        }
        Console.WriteLine("which guest do you want to remove an episode from?");
        
        string inputGuest = Console.ReadLine();
        
        int g;
        
        while (!Int32.TryParse(inputGuest, out g) || (g < 0 && g > guests.Count()))
        {
            Console.WriteLine("your input was not correct, try again");
        }
        
        
        Console.WriteLine("which guest do you want to add to the episode?");

        IEnumerable<Episode> episodesOfGuest = _manager.GetEpisodesOfGuests(g);
        foreach (var e in episodesOfGuest)
        {
            Console.WriteLine($"{e.Id}: {e.EpisodeTitle}");
        }
        
        Console.WriteLine("which episode do you want to delete?");
        string inputEpisode = Console.ReadLine();
        
        int eps;
        
        while (!Int32.TryParse(inputEpisode, out eps) || (eps < 0 && eps > episodes.Count()))
        {
            Console.WriteLine("your input was not correct, try again");
        }
        
        _manager.DeleteEpisodeParticipation(eps, g);
        Console.WriteLine($"episode participation deleted with episode Id {eps} and guest Id {g}");
    }
}