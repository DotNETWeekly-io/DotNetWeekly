namespace ResultSample
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ValidationExceptionMiddleware(RequestDelegate request)
        {
            _request=request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (InvalidOperationException e)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(e.Message);
            }
        }
    }
}
