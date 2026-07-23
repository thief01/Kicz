using KichBackendApp.Integrations.Twitch.DTOs;

namespace KichBackendApp.Integrations.Twitch;

public class TwitchAuthClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    private string? _accessToken;


    public TwitchAuthClient(
        HttpClient http,
        IConfiguration config)
    {
        _http = http;
        _config = config;
    }


    public async Task<string> GetToken()
    {
        if (_accessToken != null)
            return _accessToken;


        var response = await _http.PostAsync(
            "oauth2/token",
            new FormUrlEncodedContent(
            [
                new KeyValuePair<string,string>(
                    "client_id",
                    _config["Twitch:ClientId"]!
                ),

                new KeyValuePair<string,string>(
                    "client_secret",
                    _config["Twitch:ClientSecret"]!
                ),

                new KeyValuePair<string,string>(
                    "grant_type",
                    "client_credentials"
                )
            ])
        );


        response.EnsureSuccessStatusCode();


        var data =
            await response.Content
                .ReadFromJsonAsync<TwitchTokenResponse>();


        _accessToken = data!.AccessToken;

        return _accessToken;
    }
}