import {useState} from "react";
import {useRouter} from "next/navigation";
import {createPost} from "@/src/services/post.service";

export function useCreatePost() {
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    const handleCreatePost = async (content: string, imageUrl: string) => {
        setLoading(true);
        setError('')

        try {
            await createPost(content, imageUrl);
        }
        catch {
            setError(error);
        }
        finally {
            setLoading(false);
        }
    }

    return {handleCreatePost, error, loading}
}