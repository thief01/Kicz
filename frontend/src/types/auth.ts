export interface AuthResponse
{
    isAuthenticated: boolean;
    userId?: string,
    userName?: string
}

export interface LoginInput
{
    email: string,
    password: string,
}

export interface LoginResponse
{
    token: string,
}