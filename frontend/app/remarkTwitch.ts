import { visit } from "unist-util-visit";

export function remarkTwitch() {
    return (tree: any) => {
        visit(tree, (node) => {
            // :::twitch CLIP_ID
            if (node.type === "containerDirective" && node.name === "twitch") {
                const clipId = node.children?.[0]?.value?.trim();

                if (!clipId) return;

                node.type = "mdxJsxFlowElement";
                node.name = "TwitchClip";
                node.attributes = [
                    { type: "mdxJsxAttribute", name: "clipId", value: clipId },
                ];
                node.children = [];
            }
        });
    };
}
