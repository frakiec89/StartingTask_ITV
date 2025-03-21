using System.Security.Claims;
using System.Text;

internal class MyBasicAuthentication
{
    private readonly RequestDelegate _next; 

    public MyBasicAuthentication(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization") ==false)
        {
            context.Response.Headers.Add("WWW-Authenticate", "Basic");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();

        if (authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentialBytes = Convert.FromBase64String(token);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            
            var username = credentials[0];
            var password = credentials[1];

            if (IsAuthorized(username, password))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, "Basic");
                context.User = new ClaimsPrincipal(identity);
            }
            else
            {
                context.Response.Headers.Add("WWW-Authenticate", "Basic");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await _next(context);
    }

    private bool IsAuthorized(string username, string password)
    {
        if(username == "admin" && password == "123") // todo Mock авторизация - дописать запрос  в  дб   
            return true;
        else
            return false;
    }
}