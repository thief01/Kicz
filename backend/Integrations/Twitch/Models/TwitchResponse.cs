namespace KichBackendApp.Integrations.Twitch.Models;

public record TwitchResponse<T>(
    List<T> Data,
    TwitchPaginationDto? Pagination
);

public record TwitchPaginationDto(
    string? Cursor
);