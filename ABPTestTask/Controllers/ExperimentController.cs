using ABPTestTask.DB;
using ABPTestTask.Helpers;
using ABPTestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABPTestTask.Controllers;

[ApiController]
[Route("experiment")]
public class ExperimentController : Controller
{
    private readonly ExperimentRepository _experimentRepository;
    private readonly DeviceRepository _deviceRepository;

    public ExperimentController(ExperimentRepository experimentRepository, DeviceRepository deviceRepository)
    {
        _experimentRepository = experimentRepository;
        _deviceRepository = deviceRepository;
    }

    [HttpGet]
    [Route("button-color")]
    public async Task<ActionResult<ResponseModel<string>>> GetButtonColor(string deviceToken)
    {
        try
        {
            var device = await _deviceRepository.GetByDeviceToken(deviceToken);
            var colorExperiment = await _experimentRepository.GetByDeviceId<ColorExperiment>(device.Id);
            if (colorExperiment != null)
                return Ok(new ResponseModel<string>("button_color", colorExperiment.Color));


            string color = RandomGenerator.GetRandomButtonColor(Constants.Colors);

            await _experimentRepository.InsertColorExperiment(new ColorExperiment()
                { DeviceId = device.Id, Color = color });

            return Ok(new ResponseModel<string>("button_color", color));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest("some error");
        }
    }

    [HttpGet]
    [Route("price")]
    public async Task<ActionResult<ResponseModel<decimal>>> GetPrice(string deviceToken)
    {
        try
        {
            var device = await _deviceRepository.GetByDeviceToken(deviceToken);
            var priceExperiment = await _experimentRepository.GetByDeviceId<PriceExperiment>(device.Id);
            if (priceExperiment != null)
                return Ok(new ResponseModel<decimal>("price", priceExperiment.Price));

            PriceOption randomPrice = RandomGenerator.SelectPriceWithProbability(Constants.PriceOptions);
            var insertedPriceExperiment = await _experimentRepository.InsertPriceExperiment(new PriceExperiment
                { Price = randomPrice.Price, DeviceId = device.Id });
            return Ok(new ResponseModel<decimal>("price", insertedPriceExperiment.Price));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest("some error");
        }
    }
}