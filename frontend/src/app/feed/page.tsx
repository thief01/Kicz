'use client';

import { useEffect, useState } from 'react';
import PostForm from "@/src/components/post/PostForm";
import NewPostForm from "@/src/components/post/NewPostForm";
import {useFeed} from "@/src/hooks/useFeed";
import {getToken} from "@/src/utils/token";
import LoaderCircle from "@/src/components/general/Loader";
import {checkAuthStatus} from "@/src/services/auth.service";


export default function Feed() {

    const {posts, error, loading, refetch} = useFeed();

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    useEffect(() => {
        async function load() {
            const result = await checkAuthStatus();
            setIsAuthenticated(result);
        }
        load();
    }, []);

    const [isLoggedIn] = useState(() => {
        refetch();
        return !!getToken();
    });

    if (loading) return <LoaderCircle/>;
    if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

    return (
        <div className="container mx-auto p-4 max-w-2xl">
            <h1 className="text-2xl font-bold mb-6 text-center">FEED</h1>

            {isAuthenticated && <NewPostForm onSuccess={refetch} />}

            {posts.length === 0 ? (
                <p className="text-gray-500 text-center text-2xl">Not found posts :(</p>
            ) : (
                posts.map((post) => {
                    return (
                        <PostForm
                            key={post.id}
                            postResponse={post}
                        />
                    );
                })
            )}
        </div>
    );
}