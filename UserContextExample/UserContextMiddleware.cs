using UserContextExample.Controllers;

namespace UserContextExample;

public class UserContextMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext, UserContext userContext)
    {
        userContext.UserId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        userContext.Roles = httpContext.User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

        await next(httpContext);
    }
}