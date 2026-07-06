"use client";

import Link from "next/link";

export default function Navbar() {
    return (
        <nav style={{ padding: "10px", display: "flex", gap: "10px" }}>
            <Link href="/feed">Home</Link>
            <Link href="/login">Login</Link>
            <Link href="/register">Register</Link>
        </nav>
    );
}