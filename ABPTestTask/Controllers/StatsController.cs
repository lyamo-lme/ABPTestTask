using ABPTestTask.DB;
using ABPTestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABPTestTask.Controllers;

[Route("stats")]
public class StatsController : Controller
{
    private readonly ExperimentRepository _experimentRepository;

    public StatsController(ExperimentRepository experimentRepository)
    {
        _experimentRepository = experimentRepository;
    }
    [HttpGet]
    [Route("chart")]
    public async Task<ActionResult> GetChartPage()
    {
        Experiments experiments = new()
        {
            ColorExperiments = await _experimentRepository.GetExperiments<ColorExperiment>(),
            PriceExperiments = await _experimentRepository.GetExperiments<PriceExperiment>()
        };
        return View("Chart", experiments);
    }
}