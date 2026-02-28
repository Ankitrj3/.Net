using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightSearchEngine.Models;

public class SearchViewModel
{
    [Required]
    public string Source { get; set; } = string.Empty;

    [Required]
    public string Destination { get; set; } = string.Empty;

    [Required]
    [Range(1, 10)]
    public int NumberOfPersons { get; set; } = 1;

    public SelectList? SourceList { get; set; }
    public SelectList? DestinationList { get; set; }
}