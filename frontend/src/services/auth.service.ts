import {getToken, removeToken, setToken} from "@/src/utils/token";

export const login = async ({email, password}: LoginInput): Promise<LoginResponse> => {
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({email, password}),
    })

    if(!res.ok) throw new Error(`Failed to login with email ${email}`);

    const data: LoginResponse = await res.json();
    setToken(data.token);
    return data;
}

export const register = async ({email, password, displayName}: RegisterInput): Promise<RegisterResponse> =>{
    const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/auth/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({email, password, displayName}),
    })

    if(!res.ok) throw new Error(`Failed to register with email ${email}`);
    const data: RegisterResponse = await res.json();
    setToken(data.token);
    return data;
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
        const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/auth/verify`,
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

