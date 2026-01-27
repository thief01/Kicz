'use client';

import { useEffect } from 'react';

export default function ClientLayout({
                                         children,
                                     }: {
    children: React.ReactNode;
}) {
    useEffect(() => {
        // Fix dla Brave Browser
        if (typeof window !== 'undefined' && window.ethereum?.isBraveWallet) {
            console.log('Brave Wallet wykryty');
        }
    }, []);

    return <>{children}</>;
}