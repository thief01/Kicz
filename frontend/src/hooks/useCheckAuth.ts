"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";

export function useCheckAuth() {
    const router = useRouter();

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token) {
            router.replace("/auth/profile");
        }
    }, [router]);
}