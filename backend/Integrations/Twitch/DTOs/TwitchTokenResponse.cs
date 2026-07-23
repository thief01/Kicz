using System.Text.Json.Serialization;

namespace KichBackendApp.Integrations.Twitch.DTOs;

public record TwitchTokenResponse(
    [property: JsonPropertyName("access_token")]
    string AccessToken,

    [property: JsonPropertyName("expires_in")]
    int ExpiresIn
);