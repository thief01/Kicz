// lib/remarkTwitchPlugin.js
import { visit } from 'unist-util-visit';

export function remarkTwitchPlugin() {
    return (tree) => {
        visit(tree, (node) => {
            console.log('Node type:', node.type, 'Node name:', node.name); // Debug

            if (
                node.type === 'containerDirective' ||
                node.type === 'leafDirective' ||
                node.type === 'textDirective'
            ) {
                if (node.name === 'twitch') {
                    console.log('Found twitch directive!', node); // Debug

                    const data = node.data || (node.data = {});
                    const clipId = node.attributes?.clip || '';
                    const parent = node.attributes?.parent || 'localhost';

                    console.log('ClipId:', clipId, 'Parent:', parent); // Debug

                    data.hName = 'iframe';
                    data.hProperties = {
                        src: `https://clips.twitch.tv/embed?clip=${clipId}&parent=${parent}`,
                        frameBorder: '0',
                        allowFullScreen: true,
                        scrolling: 'no',
                        height: '378',
                        width: '620'
                    };
                }
            }
        });
    };
}