'use client'

import { useState } from "react";
import { useLogin } from '@/src/hooks/useLogin'


export default function LoginForm() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const { handleLogin, error, loading } = useLogin()

    const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        handleLogin(email, password)
    }

    return (
        <form onSubmit={onSubmit} className="p-4 max-w-md mx-auto">
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
            <button
                type="submit"
                disabled={loading}
                className="bg-green-500 text-white px-4 py-2 rounded disabled:opacity-50"
            >
                {loading ? 'Logowanie...' : 'Login'}
            </button>
        </form>
    )
}