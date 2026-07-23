namespace KichBackendApp.Integrations.Twitch.DTOs;

public record TwitchStreamDto
{
    public string Id { get; set; } = "";
    public string UserId { get; set; } = "";
    public string UserName { get; set; } = "";

    public string GameName { get; set; } = "";
    public string Title { get; set; } = "";

    public int ViewerCount { get; set; }

    public DateTime StartedAt { get; set; }
}