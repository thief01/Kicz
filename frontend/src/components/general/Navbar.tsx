"use client";

import Link from "next/link";
import {checkAuthStatus} from "@/src/services/auth.service";
import {useEffect, useState} from "react";
import {usePathname} from "next/navigation";

export default function Navbar() {
    const pathName = usePathname();
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        checkAuthStatus()
            .then(setIsAuthenticated);
    }, [pathName]);


    return (
        <nav className="relative bottom-0 flex items-center p-2.5 bg-gray-800">
            <div className="flex-1">
                <h1 className="text-2xl font-bold ml-6">JFN</h1>
            </div>

            <div className="absolute left-1/2 -translate-x-1/2 flex gap-10">
                <Link href="/feed" className="link-button link-button-primary">Feed</Link>
            </div>

            <div className="flex flex-1 justify-end gap-6 mr-6">
                {!isAuthenticated ? (
                    <>
                        <Link href="/auth/login" className="link-button link-button-primary">Login</Link>
                        <Link href="/auth/register" className="link-button link-button-primary">Register</Link>
                    </>
                ) : (
                    <>
                        <Link href="/auth/profile" className="link-button link-button-primary">Profile</Link>
                        <Link href="/auth/logout" className="link-button link-button-primary">Logout</Link>
                    </>
                )}
            </div>
        </nav>
    );
}