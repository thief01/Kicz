export function TwitchClip({ clipId }: { clipId: string }) {
    return (
        <iframe
            src={`https://clips.twitch.tv/embed?clip=${clipId}&parent=yourdomain.com`}
            width={620}
            height={378}
            allowFullScreen
        />
    );
}