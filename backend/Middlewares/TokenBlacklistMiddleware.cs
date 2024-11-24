using Enyim.Caching;

namespace bioinsumos_asproc_backend.Middlewares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemcachedClient _memcachedClient;

        public TokenBlacklistMiddleware(RequestDelegate next, IMemcachedClient memcachedClient)
        {
            _next = next;
            _memcachedClient = memcachedClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var isBlacklisted = await _memcachedClient.GetAsync<bool>(token);

                if (isBlacklisted.Value)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }

    public static class TokenBlacklistMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenBlacklistMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenBlacklistMiddleware>();
        }
    }
}