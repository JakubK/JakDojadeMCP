namespace JakDojadeMCP.Server.Models;

public record ScheduleTable(Location? Location, IEnumerable<ScheduleLine> ScheduleLines);