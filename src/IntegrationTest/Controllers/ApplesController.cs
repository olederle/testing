using Microsoft.AspNetCore.Mvc;
using UnitTests.Apples;

namespace IntegrationTests.Controller;

[ApiController]
[Route("/apples")]
public class ApplesController : ControllerBase
{
    private readonly AppleService appleService;

    public ApplesController(AppleService appleService)
    {
        this.appleService = appleService;
    }

    [HttpGet]
    public ActionResult<List<Apple>> GetApples()
    {
        return Ok(appleService.GetApplesSortedByName());
    }
}
