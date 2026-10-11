using Microsoft.AspNetCore.Mvc;

namespace UserContextExample.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController(ApplicationService application, UserContext userContext) : ControllerBase
{
    [HttpGet]
    public IActionResult List()
    {
        application.PerformApplicationOperation();
        return Ok();
    }
}

public class ApplicationService(Layer2Service layer2)
{
    public void PerformApplicationOperation()
    {
        layer2.PerformLayerOperation();
    }
}


public class Layer2Service(Layer3Service layer3)
{
   public void PerformLayerOperation()
    {
        layer3.PerformDeepestOperation();
    }
}

public class Layer3Service(UserContext userContext)
{
    public void PerformDeepestOperation()
    {
        _ = userContext;
    }
}

public class UserContext
{
    public string? UserId { get; set; }

    public List<string> Roles { get; set; } = [];
}