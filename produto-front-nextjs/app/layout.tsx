import * as React from "react";
import './globals.css';
import { NextUIProvider } from '@nextui-org/react';

export const metadata = {
  title: 'Produto Frontend',
  description: 'Gerenciamento de produtos',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="pt-BR">
      <body>
        <NextUIProvider>{children}</NextUIProvider>
      </body>
    </html>
  );
}
