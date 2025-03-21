using System.Security.Claims;
using System.Text;

internal class MyBasicAuthentication
{

    private readonly IConfiguration _configuration;
    private readonly RequestDelegate _next; 

    public MyBasicAuthentication(RequestDelegate next , IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
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
        string _username = _configuration["Authorization:Username"]; // todo Mock авторизация - дописать запрос  в  дб   
        string _password = _configuration["Authorization:Password"];

        if (username == _username && password == _password) 
            return true;
        else
            return false;
    }
}