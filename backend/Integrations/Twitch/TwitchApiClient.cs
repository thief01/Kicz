using KichBackendApp.Integrations.Twitch.DTOs;
using KichBackendApp.Integrations.Twitch.Models;

namespace KichBackendApp.Integrations.Twitch;

public class TwitchApiClient
{
    private readonly HttpClient _http;
    private readonly TwitchAuthClient _auth;


    public TwitchApiClient(
        HttpClient http,
        TwitchAuthClient auth)
    {
        _http = http;
        _auth = auth;
    }


    public async Task<TwitchStreamDto?> GetStream(
        string username)
    {
        var token = await _auth.GetToken();


        _http.DefaultRequestHeaders.Remove(
            "Authorization"
        );

        _http.DefaultRequestHeaders.Add(
            "Authorization",
            $"Bearer {token}"
        );


        _http.DefaultRequestHeaders.Remove(
            "Client-ID"
        );

        _http.DefaultRequestHeaders.Add(
            "Client-ID",
            "dysw3kk0acvfu7lvvhn7mtpvp2h01z"
        );


        var response =
            await _http.GetFromJsonAsync<
                TwitchResponse<TwitchStreamDto>
            >(
                $"helix/streams?user_login={username}"
            );


        return response?.Data.FirstOrDefault();
    }
}