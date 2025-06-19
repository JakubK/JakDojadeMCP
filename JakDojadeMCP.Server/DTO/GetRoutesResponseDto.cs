using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.DTO;

public record GetRoutesResponseDto(IEnumerable<Route> Routes);
