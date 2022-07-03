using Microsoft.AspNetCore.Builder;
using JournalAPI;
using Microsoft.Extensions.DependencyInjection;
using JournalAPI.Common;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();