using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Helpers;

public static class QueryStringBuilder
{
    public static string ToQueryString(FindRoute obj)
    {
        var strings = new List<string>
        {
            EncodeParam("fc", $"{obj.StartPointLatitude}:{obj.StartPointLongitude}"),
            EncodeParam("tc", $"{obj.EndPointLatitude}:{obj.EndPointLongitude}"),
            EncodeParam("agg", obj.Agglomeration),
            EncodeParam("d", obj.Date),
            EncodeParam("h", obj.Hour),
            EncodeParam("ia", obj.IsArrival),
            EncodeParam("rc", obj.RouteCount),
            EncodeParam("ri", obj.RouteIndex),
            EncodeParam("t", obj.Type),
            EncodeParam("rcoor", obj.IncludeRoadCoordinates),
            EncodeParam("test", obj.Test),
            EncodeParam("aac", obj.AvoidChanges),
            EncodeParam("aab", obj.AvoidBuses),
            EncodeParam("apv", obj.ProhibitedVehicles),
            EncodeParam("apo", obj.ProhibitedOperators),
            EncodeParam("aax", obj.AvoidExpresses),
            EncodeParam("aaz", obj.AvoidZonal),
            EncodeParam("aol", obj.OnlyLowFloor),
            EncodeParam("aro", obj.RoadsOn),
            EncodeParam("aal", obj.ProhibitedLines),
            EncodeParam("apl", obj.PreferLines)
        }.Where(x => !string.IsNullOrEmpty(x));
        
        return string.Join("&", strings);
    }

    private static string EncodeParam(string key, object? value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        
        return $"{key}={value}";
    }
}