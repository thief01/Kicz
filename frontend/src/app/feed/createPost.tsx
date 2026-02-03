'use client';

import {useState} from 'react';
import MarkdownPost from '@/src/components/MarkdownPost';
import remarkDirective from "remark-directive";
import {remarkTwitchPlugin} from "@/src/lib/markdown/remarkTwitchPlugin";
import NewPostForm from "@/src/components/post/NewPostForm";

interface CreatePostProps {
    onPostCreated?: () => void; // Callback po stworzeniu posta
}

export default function CreatePost({onPostCreated}: CreatePostProps) {
    const [content, setContent] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [preview, setPreview] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!content.trim()) {
            setError('Wpisz treść posta');
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const token = localStorage.getItem('token');

            if (!token) {
                setError('Musisz być zalogowany');
                return;
            }

            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Post`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({
                    content: content,
                    imageUrl: '',
                    scheduledFor: new Date().toISOString()
                })
            });

            if (!res.ok) {
                throw new Error('Nie udało się utworzyć posta');
            }

            // Wyczyść pole i wywołaj callback
            setContent('');
            if (onPostCreated) {
                onPostCreated();
            }
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Wystąpił błąd');
        } finally {
            setLoading(false);
        }
    };


    return (
        <NewPostForm/>
    );
}