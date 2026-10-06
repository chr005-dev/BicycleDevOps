using BicycleApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<BicycleService>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    message = "Bicycle API er klar",
    bicycles = "/api/bicycles"
}));

app.MapControllers();

app.Run();