import Script from "next/script";

declare global {
    interface Window {
        Twitch: any;
    }
}

interface TwitchStreamProps
{
    streamName: string;
    onlyVideo?: boolean;
}

export default function TwitchStream({streamName, onlyVideo = true} : TwitchStreamProps) {
    return (
        <div>
            <div id="twitch-embed"/>
            <Script
                src="https://embed.twitch.tv/embed/v1.js"
                onLoad={() => {
                    new window.Twitch.Embed("twitch-embed", {
                        channel: streamName,
                        width: "100%",
                        height: 600,
                        parent: ["localhost"],
                        layout: onlyVideo ? "video" : ""
                    });
                }}
            />
        </div>
    );
}