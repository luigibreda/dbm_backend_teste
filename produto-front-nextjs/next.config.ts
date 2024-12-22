import type { NextConfig } from "next";

module.exports = {
    env: {
      NEXT_PUBLIC_API_URL: process.env.NEXT_PUBLIC_API_URL || 'https://localhost:44330',  // URL da API
    },
  };

const nextConfig: NextConfig = {
    reactStrictMode: true, // Ajuda a identificar problemas de renderização
    swcMinify: true,       // Melhora a performance do build e comprime arquivos
    async rewrites() {
        return [
            {
                source: '/api/:path*',
                destination: 'https://localhost:44330/api/:path*', // Proxy para API para evitar problemas de CORS localmente! #ficaadica
            },
        ];
    },
};

export default nextConfig;
