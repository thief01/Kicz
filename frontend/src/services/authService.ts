
interface AuthResponse
{
    isAuthenticated: boolean;
    userId?: string;
    userName?: string;
}

export const checkAuthStatus = async(): Promise<boolean> =>
{
    const token = getToken();

    if(!token)
    {
        return false;
    }

    try
    {
        const response = await fetch(`${process.env.NEXT_PUBLIC_SERVICE_URL}}/api/auth/verify)`,
            {
                method: "GET",
                headers: {
                    'Authorization': `Bearer ${token}`,
                }
            });
        if(response.ok)
        {
            const data: AuthResponse = await response.json();
            return data.isAuthenticated;
        }
        else
        {
            removeToken();
            return false;
        }
    }
    catch (error)
    {
        console.error("Auth check failed: ", error);
        return false;
    }
};

export const getToken = (): string | null => {
    return localStorage.getItem("token");
};

export const setToken = (token: string): void  =>{
    localStorage.setItem('token', token);
}

export const removeToken = (): void =>{
    localStorage.removeItem("token");
}