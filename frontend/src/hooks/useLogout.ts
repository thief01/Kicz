import {logout} from "@/src/services/auth.service";


export function useLogout()
{
    const handleLogout = async () =>
    {
        await logout();
    }
    return {handleLogout};
}