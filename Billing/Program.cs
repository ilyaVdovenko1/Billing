using Billing.Interfaces;
using Billing.Models;
using Billing.Repository.Services;
using Billing.Services;

var builder = WebApplication.CreateBuilder(args);

var storagePath = builder.Configuration["UsersStoragePath"];
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
var service = builder.Services.AddRepository(new FileStream(storagePath, FileMode.Open));
builder.Services.AddSingleton<IAppService>(new AppService(service, new IdGenerator(0)));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BillingService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();
