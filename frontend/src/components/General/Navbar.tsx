"use client";

import Link from "next/link";
import {checkAuthStatus} from "@/src/services/auth.service";
import {useEffect, useState} from "react";

export default function Navbar() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    useEffect(() => {
        async function load() {
            const result = await checkAuthStatus();
            setIsAuthenticated(result);
            load();
        }
    }, []);
    return (
        <nav style={{padding: "10px", display: "flex", gap: "10px", justifyContent: "center", alignItems: "center"}}>
            <Link href="/feed">Home</Link>

            {!isAuthenticated ? (<>
                <Link href="/auth/login">Login</Link>
                <Link href="/auth/register">Register</Link>
            </>) : (<> <Link href="/auth/profile">Profile</Link> </>)}
        </nav>
    );
}