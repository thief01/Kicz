"use client"

import {useLogout} from "@/src/hooks/useLogout";

export default function Logout()
{
    const {handleLogout} = useLogout();

    return (
        <button onClick={handleLogout}>
            Go to feed
        </button>
    );
}