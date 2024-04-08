using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PM.BL.Domain;

public class Host
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int YearFirstPublished { get; set; }
    public double Rating { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }
    public ICollection<Episode> Episodes { get; set; }

    public Host(int id, string name, int yearFirstPublished, double rating, Gender gender)
    {
        Id = id;
        Name = name;
        YearFirstPublished = yearFirstPublished;
        Rating = rating;
        Gender = gender;
        Episodes = new List<Episode>();
    }
    
    public Host(string name, int yearFirstPublished, double rating, Gender gender)
    {
        Name = name;
        YearFirstPublished = yearFirstPublished;
        Rating = rating;
        Gender = gender;
        Episodes = new List<Episode>();
    }
    
    public Host()
    {
        Episodes = new List<Episode>();
    }
}