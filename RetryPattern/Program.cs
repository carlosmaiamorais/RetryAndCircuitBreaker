using Polly;
using RetryPattern.CrossCutting;
using RetryPattern.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IClienteAppService, ClienteAppService>()
    .AddPolicyHandler(PollyConfig.WaitAndRetryPolicy())

    .AddTransientHttpErrorPolicy(
       p => p.CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
