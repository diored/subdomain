WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json")
	.AddUserSecrets("930d7287-a0ec-4b4f-a966-8a70c29e4a05")
	.AddEnvironmentVariables();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.MapGet("", async context =>
{
	string url = app.Configuration.GetSection("Redirects")[context.Request.Host.Host];
	if (url != null)
	{
		context.Response.Redirect(url);
	}
	else
	{
		await context.Response.WriteAsync("Hello world!");
	}
});

app.Run();