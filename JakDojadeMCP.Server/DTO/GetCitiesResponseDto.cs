using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.DTO;

public record GetCitiesResponseDto(IEnumerable<City> Cities);