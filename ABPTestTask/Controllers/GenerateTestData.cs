using ABPTestTask.DB;
using ABPTestTask.Helpers;
using ABPTestTask.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ABPTestTask.Controllers;

[ApiController]
[Route("api/test")]
public class GenerateTestData : Controller
{
    private readonly ExperimentRepository _experimentRepository;
    private readonly DeviceRepository _deviceRepository;

    public GenerateTestData(ExperimentRepository experimentRepository, DeviceRepository deviceRepository)
    {
        _experimentRepository = experimentRepository;
        _deviceRepository = deviceRepository;
    }

    [HttpGet]
    [Route("data")]
    public async Task<ActionResult> GenerateData(int countDevices)
    {
        try
        {
            for (int i = 0; i < countDevices; i++)
            {
                string randomString = RandomGenerator.GenerateRandomGeneticString(8);
                while (await _deviceRepository.GetByDeviceToken(randomString) != null)
                {
                    randomString = RandomGenerator.GenerateRandomGeneticString(8);
                }

                var device = await _deviceRepository.Insert(new Device { DeviceToken = randomString });
                
                string color = RandomGenerator.GetRandomButtonColor(Constants.Colors);
                await _experimentRepository.InsertColorExperiment(new ColorExperiment()
                    { DeviceId = device.Id, Color = color });
                
                PriceOption randomPrice = RandomGenerator.SelectPriceWithProbability(Constants.PriceOptions);
                await _experimentRepository.InsertPriceExperiment(new PriceExperiment
                    { Price = randomPrice.Price, DeviceId = device.Id });
            }

            return Ok("generated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}