using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.DTO;

public record GetLocationsResponseDto(int ResultCount, IEnumerable<Location> Locations);
