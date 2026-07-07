'use client'

import {useState} from "react";
import {useLogin} from '@/src/hooks/useLogin'
import Navbar from "@/src/components/general/Navbar";
import {useRouter} from "next/navigation";


export default function LoginForm() {
    const router = useRouter();
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const {handleLogin, error, loading} = useLogin()

    const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        handleLogin(email, password)
        router.refresh();
    }

    return (
        <div>
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
                    className="btn btn-primary"
                >
                    {loading ? 'Logging in...' : 'Login'}
                </button>
            </form>
        </div>
    )
}