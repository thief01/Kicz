import {useState} from "react";
import {useRouter} from "next/navigation";
import {getPost} from "@/src/services/post.service";

export function useGetPost() {
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    const handleGetPost = async (id: number) => {
        try {
            await getPost(id);
            setLoading(false);
        } catch {
            setError(error);
        } finally {
            setLoading(false);
        }
    }

    return {handleGetPost, error, loading};
}