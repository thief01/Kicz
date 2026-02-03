'use client';

import { useEffect, useState } from 'react';
import PostForm from "@/src/components/post/PostForm";
import CreatePost from '@/src/app/feed/createPost';
import {getToken} from "@/src/utils/token";

interface PostType {
    id: number;
    content: string;
    userDisplayName: string;
    imageUrl: string;
    createdAt: string;
    scheduledFor: string;
    isPublished: boolean;
    userId: string;
}

async function getPosts(): Promise<PostType[]> {  // ⬅️ Usuń parametr token
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

export default function Home() {
    const [posts, setPosts] = useState<PostType[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const fetchPosts = async () => {
        try {
            const data = await getPosts();  // ⬅️ Bez tokena
            setPosts(data);
        } catch (err) {
            console.error('Błąd:', err);
            setError(err instanceof Error ? err.message : 'Nieznany błąd');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        // Sprawdź czy user jest zalogowany (do wyświetlenia formularza)
        const token = localStorage.getItem('token');
        setIsLoggedIn(!!token);

        fetchPosts();
    }, []);

    const handlePostCreated = () => {
        fetchPosts();
    };

    if (loading) return <div className="p-4">Ładowanie...</div>;
    if (error) return <div className="p-4 text-red-500">Błąd: {error}</div>;

    return (
        <div className="container mx-auto p-4 max-w-2xl">
            <h1 className="text-2xl font-bold mb-6">Feed</h1>
            <CreatePost/>

            {/* Pokaż formularz tylko zalogowanym */}
            {isLoggedIn && <CreatePost onPostCreated={handlePostCreated} />}

            {posts.length === 0 ? (
                <p className="text-gray-500 text-center">Brak postów</p>
            ) : (
                posts.map((post) => (
                    <PostForm
                        key={post.id}
                        username={post.userDisplayName}
                        message={post.content}
                        createdAt={post.createdAt}
                    />
                ))
            )}
        </div>
    );
}