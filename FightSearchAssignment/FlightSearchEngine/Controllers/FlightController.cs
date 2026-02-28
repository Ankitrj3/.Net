using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using FlightSearchEngine.Data;
using FlightSearchEngine.Models;

namespace FlightSearchEngine.Controllers;

public class FlightController : Controller
{
    private readonly DatabaseHelper _db;

    public FlightController(DatabaseHelper db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var model = new SearchViewModel();
        await PopulateDropdownsAsync(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SearchFlights(SearchViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync(model);
            return View("Index", model);
        }

        try
        {
            var flights = await _db.SearchFlightsAsync(
                model.Source,
                model.Destination,
                model.NumberOfPersons
            );

            var results = new ResultsViewModel
            {
                FlightResults = flights,
                SearchSummary = $"{model.Source} → {model.Destination} ({model.NumberOfPersons} person(s))"
            };

            return View("Results", results);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error searching flights: {ex.Message}");
            await PopulateDropdownsAsync(model);
            return View("Index", model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync(model);
            return View("Index", model);
        }

        try
        {
            var packages = await _db.SearchFlightsWithHotelsAsync(
                model.Source,
                model.Destination,
                model.NumberOfPersons
            );

            var results = new ResultsViewModel
            {
                FlightHotelResults = packages,
                SearchSummary = $"{model.Source} → {model.Destination} ({model.NumberOfPersons} person(s))"
            };

            return View("Results", results);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error searching packages: {ex.Message}");
            await PopulateDropdownsAsync(model);
            return View("Index", model);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    private async Task PopulateDropdownsAsync(SearchViewModel model)
    {
        try
        {
            model.SourceList = new SelectList(await _db.GetSourcesAsync());
            model.DestinationList = new SelectList(await _db.GetDestinationsAsync());
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Database error: {ex.Message}";
        }
    }
}