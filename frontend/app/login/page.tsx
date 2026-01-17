'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';

export default function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const router = useRouter();

    async function handleLogin(e: React.FormEvent) {
        e.preventDefault();

        const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/auth/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });

        if (res.ok) {
            const data = await res.json();
            // data.token to JWT
            localStorage.setItem('token', data.token);
            router.push('/feed');
        } else {
            setError('Niepoprawny login lub hasło');
        }
    }

    return (
        <form onSubmit={handleLogin} className="p-4 max-w-md mx-auto">
            <h1 className="text-2xl mb-4">Login</h1>
            {error && <p className="text-red-500">{error}</p>}
            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="border p-2 mb-2 w-full"
            />
            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="border p-2 mb-2 w-full"
            />
            <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded">
                Login
            </button>
        </form>
    );
}
