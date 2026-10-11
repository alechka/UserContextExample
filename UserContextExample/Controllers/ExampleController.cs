using Microsoft.AspNetCore.Mvc;

namespace UserContextExample.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController(ApplicationService application) : ControllerBase
{
    [HttpGet]
    public IActionResult List()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        var permissions = User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
        
        var userContext = new UserContext
        {
            UserId = userId,
            Roles = permissions
        };

        application.PerformApplicationOperation(userContext);

        return Ok();
    }
}

public class ApplicationService(Layer2Service layer2)
{
    public void PerformApplicationOperation(UserContext userContext)
    {
        layer2.PerformLayerOperation(userContext);
    }
}


public class Layer2Service(Layer3Service layer3)
{
   public void PerformLayerOperation(UserContext userContext)
    {
        layer3.PerformDeepestOperation(userContext);
    }
}

public class Layer3Service
{
    public void PerformDeepestOperation(UserContext userContext)
    {
        // some logic here        
    }
}

public class UserContext
{
    public string? UserId { get; set; }

    public List<string> Roles { get; set; } = [];
}