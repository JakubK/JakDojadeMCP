namespace JakDojadeMCP.Server.Models;

public record SimpleCoordinate(string Latitude, string Longitude)
{
    public override string ToString()
    {
        return $"{Latitude}:{Longitude}";
    }
}