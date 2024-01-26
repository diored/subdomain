WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("", (HttpContext context) =>
{
    string targetDomain = context.Request.Host.Host;

    if (app.Configuration[$"Redirects:{targetDomain}"] is { } url)
    {
        return Results.Redirect(url);
    }

    return Results.Text("Hello world!");
});

app.Run();