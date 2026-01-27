'use client';

import {useState} from 'react';
import MarkdownPost from '@/src/components/MarkdownPost';
import remarkDirective from "remark-directive";
import {remarkTwitchPlugin} from "@/src/lib/markdown/remarkTwitchPlugin";

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
        <div className="bg-black rounded-lg shadow p-4 mb-6">
            <form onSubmit={handleSubmit}>
                <textarea
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    placeholder="Wklej markdown tutaj..."
                    rows={10}
                    style={{
                        width: '100%',
                        whiteSpace: 'pre-wrap' // Ważne!
                    }}
                />

                {error && (
                    <p className="text-red-500 text-sm mt-2">{error}</p>
                )}

                <div className="flex justify-end mt-3">
                    <button
                        type="submit"
                        disabled={loading || !content.trim()}
                        className="bg-green-500text-white px-6 py-2 rounded-lg hover:bg-green-500 disabled:bg-gray-300 disabled:cursor-not-allowed transition"
                    >
                        {loading ? 'Wysyłanie...' : 'Opublikuj'}
                    </button>
                </div>
            </form>
        </div>
    );
}