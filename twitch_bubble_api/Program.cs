using System.Text.Json;
using twitch_bubble_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/test", () => { return Results.Ok(); })
    .WithName("Test")
    .WithOpenApi();

app.MapPost("/bubbles", async (SerialRequest req) =>
    {
        try
        {
            if (!SerialHelper.TryWriteSeconds(req.SerialPort, req.DurationSeconds))
            {
                throw new Exception("Serial write failure");
            }
            
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }
        return Results.Ok();
    })
    .WithName("MakeBubbles")
    .WithOpenApi();

app.Run();

