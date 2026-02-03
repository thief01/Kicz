interface AuthResponse
{
    isAuthenticated: boolean;
    userId?: string;
    userName?: string;
}

interface LoginInput
{
    email: string,
    password: string,
}

interface RegisterInput
{
    email: string,
    password: string,
    displayName: string,
}

interface LoginResponse
{
    token: string;
}

interface RegisterResponse
{
    token: string;
}