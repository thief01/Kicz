import {getToken} from "@/src/utils/token";



export const getPost = async (postId: number): Promise<PostResponse> => {
    const token = getToken();
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/post/${postId}`,
        {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`,
            },
            body: JSON.stringify(postId),
        })
    if(!res.ok) throw new Error(res.statusText);

    const data: PostResponse = await res.json();
    return data as PostResponse;
}

export const createPost = async(content: string, imageUrl: string): Promise<PostResponse> => {
    const token = getToken();
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`,
            },
            body: JSON.stringify({content, imageUrl}),
        });
    if(!res.ok) throw new Error(res.statusText);
    const data: PostResponse = await res.json();
    return data as PostResponse;
}