using OrderService.Application;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Messaging.Topology;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//to define the exhange when application starts
var initializer = app.Services.GetRequiredService<IRabbitMqTopologyInitializer>();
await initializer.InitializeAsync();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
