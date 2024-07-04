using MassTransit;
using Microsoft.EntityFrameworkCore;
using PhotoProcessingApp;
using PhotoProcessingService;
using PhotoProcessingService.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PhotoProcessingConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        cfg.ReceiveEndpoint("photo-processing-queue", e =>
        {
            e.ConfigureConsumer<PhotoProcessingConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();
