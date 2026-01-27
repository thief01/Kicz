'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';

export default function RegisterPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [displayName, setDisplayName] = useState('');
    const [error, setError] = useState('');
    const router = useRouter();

    async function handleRegister(e: React.FormEvent) {
        e.preventDefault();

        const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/auth/register`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password, displayName })
        });

        if (res.ok) {
            const data = await res.json();
            localStorage.setItem('token', data.token);
            router.push('/feed');
        } else {
            try {
                const errorData = await res.json();
                setError(errorData.message || 'Nie udało się zarejestrować');
            } catch {
                setError('Nie udało się zarejestrować');
            }
        }
    }

    return (
        <form onSubmit={handleRegister} className="p-4 max-w-md mx-auto">
            <h1 className="text-2xl mb-4">Register</h1>
            {error && <p className="text-red-500">{error}</p>}
            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="border p-2 mb-2 w-full"
                required
            />
            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="border p-2 mb-2 w-full"
                required
            />
            <input
                type="text"
                placeholder="Display Name"
                value={displayName}
                onChange={(e) => setDisplayName(e.target.value)}
                className="border p-2 mb-2 w-full"
                required
            />
            <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded">
                Register
            </button>
        </form>
    );
}
