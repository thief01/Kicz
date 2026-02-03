'use client';

import { useState } from 'react';
import PostForm from "@/src/components/post/PostForm";
import NewPostForm from "@/src/components/post/NewPostForm";
import {useFeed} from "@/src/hooks/useFeed";
import {getToken} from "@/src/utils/token";


export default function Feed() {

    const {posts, error, loading, refetch} = useFeed();

    // const [isLoggedIn] = useState(() => {
    //     const token = getToken();
    //     return !!token;
    // });


    if (loading) return <div className="p-4">Loading...</div>;
    if (error) return <div className="p-4 text-red-500">Error: {error}</div>;

    return (
        <div className="container mx-auto p-4 max-w-2xl">
            <h1 className="text-2xl font-bold mb-6">Feed</h1>

            {/*{isLoggedIn && <NewPostForm onSuccess={refetch} />}*/}

            {posts.length === 0 ? (
                <p className="text-gray-500 text-center">Not found posts</p>
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