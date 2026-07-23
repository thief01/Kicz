namespace KichBackendApp.Integrations.Twitch.DTOs;

public record TwitchStreamer
{
    public int Id { get; set; }

    public string TwitchUserId { get; set; } = "";

    public string Username { get; set; } = "";

    public bool IsLive { get; set; }

    public int ViewerCount { get; set; }
}