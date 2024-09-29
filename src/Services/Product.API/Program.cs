using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting Product API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.AddAppConfigurations();
    // Add service to the controller
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseInfrastructure();

    app.MigrateDatabase<ProductContext>((context, _) =>
    {
        ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
    }).Run();

    app.Run();
}
catch(Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Product API complete");
    Log.CloseAndFlush();
}
