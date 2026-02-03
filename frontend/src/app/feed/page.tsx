'use client';

import { useEffect, useState } from 'react';
import PostForm from "@/src/components/post/PostForm";
import {getToken} from "@/src/utils/token";
import NewPostForm from "@/src/components/post/NewPostForm";
import {useFeed} from "@/src/hooks/useFeed";

async function getPosts(): Promise<PostResponse[]> {  // ⬅️ Usuń parametr token
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Post/feed`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${getToken()}`
        },
    });

    if (!res.ok) {
        throw new Error(`HTTP error! status: ${res.status}`);
    }

    return res.json();
}

export default function Feed() {

    const {posts, error, loading, refetch} = useFeed();

    const [isLoggedIn] = useState(() => {
        const token = localStorage.getItem('token');
        return !!token;
    });


    if (loading) return <div className="p-4">Loading...</div>;
    if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

    return (
        <div className="container mx-auto p-4 max-w-2xl">
            <h1 className="text-2xl font-bold mb-6">Feed</h1>

            {/* Pokaż formularz tylko zalogowanym */}
            {isLoggedIn && <NewPostForm onSuccess={refetch} />}

            {posts.length === 0 ? (
                <p className="text-gray-500 text-center">Brak postów</p>
            ) : (
                posts.map((post) => (
                    <PostForm
                        key={post.Id}
                        postResponse={post}
                    />
                ))
            )}
        </div>
    );
}