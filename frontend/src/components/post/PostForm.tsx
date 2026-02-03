import MarkdownPost from "@/src/components/MarkdownPost";

interface PostProps {
    username: string;
    message: string;
    createdAt: string;
}
import remarkDirective from "remark-directive";
import  {remarkTwitch} from "@/src/app/remarkTwitch";
import {TwitchClip} from "@/src/components/TwitchClip";
import ReactMarkdown from 'react-markdown';
import {formatDistanceToNow} from 'date-fns';
import {pl} from 'date-fns/locale';
import {enGB} from "date-fns/locale"
import {remarkTwitchPlugin} from "@/src/lib/markdown/remarkTwitchPlugin";
import {PostResponse} from "@/src/types/post";

type PostFormProps = {
    postResponse: PostResponse
}

const formatDate = (dateString: string) => {
    return formatDistanceToNow(new Date(dateString), {
        addSuffix: true,
        locale: enGB
    });
};

export default function PostForm({postResponse}: PostFormProps) {
    return (
        <div className="border rounded-lg p-4 mb-4 shadow-sm">
            <div className="flex justify-between">
                <div className="font-bold text-lg mb-2">{postResponse.userDisplayName}</div>
                <div className="text-gray-500 text-sm mb-2">{formatDate(postResponse.createdAt)}</div>
            </div>
            <ReactMarkdown
                remarkPlugins={[remarkDirective, remarkTwitchPlugin]}
            >
                {postResponse.content}
            </ReactMarkdown>
        </div>
    );
}