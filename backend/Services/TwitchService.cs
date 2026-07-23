using KichBackendApp.Data;
using KichBackendApp.Integrations.Twitch;

namespace KichBackendApp.Services;

public class TwitchService
{
    private readonly TwitchApiClient _client;


    public TwitchService(
        TwitchApiClient client)
    {
        _client = client;
    }


    public async Task UpdateStreams()
    {
        var stream =
            await _client.GetStream("surojatka");


        if(stream == null)
        {
            Console.WriteLine("Offline");
            return;
        }


        Console.WriteLine(
            $"{stream.UserName}: {stream.ViewerCount}"
        );
    }
}