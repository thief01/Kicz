import {useState} from "react";
import {useRouter} from "next/navigation";
import {login} from '@/src/services/auth.service';

export function useLogin() {
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    const handleLogin = async (email: string, password: string) => {
        setLoading(true)
        setError('');

        try {
            await login({email, password})
            router.push('/feed')
        } catch (err) {
            setError("Failed to login");
        } finally {
            setLoading(false)
        }
    }

    return {handleLogin, error, loading}
}