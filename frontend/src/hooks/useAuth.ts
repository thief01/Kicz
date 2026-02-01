import {useEffect, useState} from "react";

//
// type AuthStatus = "loading" | "authenticated" | "unauthenticated";
//
// export function useAuth() {
//     const [user, setUser] = useState<User | null>(null);
//     const [status, setStatus] = useState<AuthStatus>("loading");
//
//     useEffect(() => {
//         async function initAuth() {
//             try {
//                 const res = await fetch("/api/auth/me");
//                 if (!res.ok) throw new Error(res.statusText);
//
//                 const data = await res.json();
//                 setUser(data);
//                 setStatus("authenticated");
//             } catch {
//                 setUser(null);
//                 setStatus("unauthenticated");
//             }
//         }
//     }, []);
//
//     async function login(email: string, password: string) {
//         const res = await fetch("/api/auth/login",
//             {
//                 method: "POST",
//                 body: JSON.stringify({email, password}),
//             });
//     }
// }
