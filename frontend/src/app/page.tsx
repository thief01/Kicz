'use client';

import Image from "next/image";
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { checkAuthStatus } from "@/src/services/auth.service";
import {useRouter} from "next/navigation";

export default function Home() {
  const router = useRouter();
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const verifyAndRedirect = async () => {
      router.push('/feed');
      setLoading(false);
    };
    verifyAndRedirect();

  }, [router]);
  if(loading) {
    return (<div style={{
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
      height: "100vh",
    }}>
      Loading...
    </div>);
  }
    return null;
}