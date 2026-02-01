'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import LoginForm from "@/src/components/auth/LoginForm";

export default function LoginPage() {
    return (
        <LoginForm/>
    );
}
