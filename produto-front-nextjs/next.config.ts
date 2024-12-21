import type { NextConfig } from "next";

const nextConfig: NextConfig = {
    reactStrictMode: true, // Ajuda a identificar problemas de renderização
    swcMinify: true,       // Melhora a performance do build e comprime arquivos
    async rewrites() {
        return [
            {
                source: '/api/:path*',
                destination: 'https://localhost:7187/api/:path*', // Proxy para API para evitar problemas de CORS localmente! #ficaadica
            },
        ];
    },
};

export default nextConfig;
