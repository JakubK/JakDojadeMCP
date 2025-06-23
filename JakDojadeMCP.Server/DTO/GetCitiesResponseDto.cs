using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.DTO;

public record GetCitiesResponseDto(IEnumerable<JakDojadeCity> Cities);