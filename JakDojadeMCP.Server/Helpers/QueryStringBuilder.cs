using JakDojadeMCP.Server.Models;

namespace JakDojadeMCP.Server.Helpers;

public static class QueryStringBuilder
{
    public static string ToQueryString(FindRoute obj)
    {
        var strings = new List<string>
        {
            EncodeParam("fc", obj.StartPoint),
            EncodeParam("tc", obj.EndPoint),
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
            EncodeList("apv", obj.ProhibitedVehicles, ','),
            EncodeList("apo", obj.ProhibitedOperators?.Cast<object>(), ','),
            EncodeParam("aax", obj.AvoidExpresses),
            EncodeParam("aaz", obj.AvoidZonal),
            EncodeParam("aol", obj.OnlyLowFloor),
            EncodeParam("aro", obj.RoadsOn),
            EncodeList("aal", obj.ProhibitedLines, ','),
            EncodeList("apl", obj.PreferLines, ',')
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

    private static string EncodeList(string key, IEnumerable<object>? values, char separator)
    {
        if (values == null)
        {
            return string.Empty;
        }
        
        var list = values.ToList();
        
        if (!list.Any())
        {
            return string.Empty;
        }

        return $"{key}={string.Join(separator, list)}";
    }
}