import {useEffect, useState} from "react";
import {useRouter} from "next/navigation";
import {getFeed} from "@/src/services/post.service";

export function useFeed() {
    const [posts, setPosts] = useState<PostResponse[]>([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    const fetchPosts = async () =>
    {
        try {
            await getFeed();
            setLoading(false);
        }
        catch
        {
            setError(error);
        }
        finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);
    return {posts, error, loading, refetch: fetchPosts};
}