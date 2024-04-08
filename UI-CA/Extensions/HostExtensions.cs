using PM.BL.Domain;

namespace PM.UI.CA.Extensions;

public static class HostExtensions
{
    public static string PrintHost(this Host host)
    {
        return $"Host: {host.Name}, 1st episode posted {host.YearFirstPublished}, average rating: {host.Rating}";
    }
}