'use client'

import { useState} from "react";
import {useCreatePost} from "@/src/hooks/useCreatePost";

export default function NewPostForm({ onSuccess }: { onSuccess?: () => void }) {
    const [content, setContent] = useState("");
    const {handleCreatePost, error, loading} = useCreatePost()

    const onSubmit = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const success = await handleCreatePost(content, "")
        if(success)
        {
            setContent('')
            onSuccess?.()
        }
    }

    return (
        <div className="bg-black rounded-lg shadow p-4 mb-6">
            <form onSubmit={onSubmit}>
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
    )
}