"use client"

import {useLogout} from "@/src/hooks/useLogout";
import {useEffect} from "react";
import LoaderCircle from "@/src/components/general/Loader";
import {useRouter} from "next/navigation";

export default function Logout()
{
    const router = useRouter();
    const {handleLogout} = useLogout();

    useEffect(() => {
        const logout = async () =>{
            await handleLogout();
            router.refresh();
            router.replace("/auth/login");
        }
        logout();
    }, []);
    return (
        <LoaderCircle/>
    );
}