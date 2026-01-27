// components/MarkdownPost.jsx
'use client';
import ReactMarkdown from 'react-markdown';
import remarkDirective from 'remark-directive';
import {remarkTwitchPlugin} from 'lib/remarkTwitchPlugin';
import rehypeRaw from 'rehype-raw';

export default function MarkdownPost({content}) {


    return (
        <div>
            <ReactMarkdown
                remarkPlugins={[remarkDirective, remarkTwitchPlugin]}
                rehypePlugins={[rehypeRaw]}
            >
                {content}
            </ReactMarkdown>

        </div>
    );
}