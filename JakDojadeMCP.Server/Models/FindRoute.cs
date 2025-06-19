namespace JakDojadeMCP.Server.Models;

public record FindRoute(SimpleCoordinate StartPoint,
    SimpleCoordinate EndPoint,
    string? Agglomeration,
    string? Date,
    string? Hour,
    bool? IsArrival,
    int? RouteCount,
    int? RouteIndex,
    RouteType? Type,
    bool? IncludeRoadCoordinates,
    bool? Test,
    bool? AvoidChanges,
    bool? AvoidBuses,
    IEnumerable<string>? ProhibitedVehicles,
    IEnumerable<string>? ProhibitedOperators,
    bool? AvoidExpresses,
    bool? AvoidZonal,
    bool? OnlyLowFloor,
    int? RoadsOn,
    IEnumerable<string>? ProhibitedLines,
    IEnumerable<string>? PreferLines
);