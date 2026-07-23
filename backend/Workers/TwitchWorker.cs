using KichBackendApp.Integrations.Twitch;
using KichBackendApp.Services;

namespace KichBackendApp.Workers;

public class TwitchWorker : BackgroundService
{
    private readonly ILogger<TwitchWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public TwitchWorker(
        ILogger<TwitchWorker> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }


    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Checking Twitch streams..."
            );

            using var scope = _scopeFactory.CreateScope();

            var twitchService =
                scope.ServiceProvider
                    .GetRequiredService<TwitchService>();

            await twitchService.UpdateStreams();


            await Task.Delay(
                TimeSpan.FromMinutes(1),
                stoppingToken
            );
        }
    }
}