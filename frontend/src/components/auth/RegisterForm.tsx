'use client'

import {useState} from "react";
import {useRegister} from "@/src/hooks/useRegister";

export default function RegisterForm()
{
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [displayName, setDisplayName] = useState("");
    const {handleRegister, error, loading} = useRegister();

    const onSubmit = (e: React.FormEvent<HTMLFormElement>) =>{
        e.preventDefault()
        handleRegister(email, password, displayName)
    }

    return (
        <form onSubmit={onSubmit} className="p-4 max-w-md mx-auto">
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
            <button type="submit"
                    className="bg-green-500 text-white px-4 py-2 rounded"
                    disabled={loading}>
                {loading ? "Registering..." : "Register"}
            </button>
        </form>
    )
}