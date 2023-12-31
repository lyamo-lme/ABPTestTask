using System;
using System.Threading.Tasks;
using ABPTestTask.DB;
using ABPTestTask.Helpers;
using ABPTestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABPTestTask.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : Controller
{
    private readonly DeviceRepository _deviceRepository;

    public DeviceController(DeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetDeviceId()
    {
        try
        {
            if (Request.Cookies.TryGetValue("device-token", out string? cookieValue))
            {
                return cookieValue;
            }

            string randomString = RandomGenerator.GenerateRandomString(8);
            while (await _deviceRepository.GetByDeviceToken(randomString) != null)
            {
                randomString = RandomGenerator.GenerateRandomString(8);
            }

            await _deviceRepository.Insert(new Device { DeviceToken = randomString });

            Response.Cookies.Append("device-token", randomString);

            return Ok(randomString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest();
        }
    }
}