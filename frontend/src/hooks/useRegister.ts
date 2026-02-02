import {useState} from "react";
import {useRouter} from "next/navigation";
import {register} from "@/src/services/auth.service";


export function useRegister()
{
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    const handleRegister = async (email: string, password: string, displayName: string) =>
    {
        setLoading(true);
        setError('');

        try {
            await register({email, password, displayName});
            router.push('/feed')
        }
        catch
        {
            setError("Failed to register")
        }
        finally {
            setLoading(false)
        }

    }

    return {handleRegister, error, loading}
}