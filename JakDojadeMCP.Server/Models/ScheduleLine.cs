namespace JakDojadeMCP.Server.Models;

public record ScheduleLine(string LineNumber, string DestinationStop, string VehicleType, bool CourseLine, IEnumerable<ScheduleLineDeparture> Departures);