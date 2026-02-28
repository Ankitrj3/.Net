namespace FlightSearchEngine.Models;

public class ResultsViewModel
{
    public List<FlightResult>? FlightResults { get; set; }
    public List<FlightHotelResult>? FlightHotelResults { get; set; }
    public bool IsFlightOnly => FlightResults != null;
    public string SearchSummary { get; set; } = string.Empty;
}
