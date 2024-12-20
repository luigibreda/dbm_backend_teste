import Image from "next/image";
import { AvatarIcon, Button } from "@nextui-org/react"; // Importando o componente Button do NextUI

export default function Home() {
  return (
    <div className="min-h-screen bg-gradient-to-r from-teal-700 to-blue-600 p-8 sm:p-20 font-[family-name:var(--font-geist-sans)] flex flex-col justify-between">
      <header className="flex justify-between items-center mb-12">
        <nav className="flex gap-6">
          <a
            className="text-white hover:underline"
            href="https://github.com/luigibreda"
            target="_blank"
            rel="noopener noreferrer"
          >
            GitHub
          </a>
          <a
            className="text-white hover:underline"
            href="https://linkedin.com/in/luigibreda"
            target="_blank"
            rel="noopener noreferrer"
          >
            LinkedIn
          </a>
        </nav>
      </header>

      <main className="flex flex-col items-center text-center text-white">
        <h1 className="text-5xl sm:text-6xl font-semibold mb-4 bg-gradient-to-r from-teal-300 via-yellow-300 to-pink-500 bg-clip-text text-transparent">
          Front-End Next.js + Back-End .NET 8
        </h1>
        <p className="text-lg sm:text-xl mb-8 max-w-3xl mx-auto">
          Conectando Front-End com Next.js e APIs robustas em .NET 8.
        </p>

        <div className="mb-8 flex justify-center gap-6">
          <Button
            as="a"
            href="produto"
            color="primary"
            size="lg"
            variant="solid"
            className="w-44 transition-all duration-300"
          >
            VER PRODUTOS
          </Button>
        </div>

        <div className="max-w-4xl mx-auto text-left">
          <h2 className="text-3xl text-yellow-300 font-semibold mb-4">Sobre mim</h2>
          <p className="text-lg mb-4">
            Sou <strong>Luigi Breda</strong>, desenvolvedor full-stack com foco em soluções escaláveis e de alta performance. Tenho experiência com tecnologias como <strong>Next.js</strong>, <strong>.NET 8</strong>, <strong>SQL Server</strong> e outras.
          </p>
          <p className="text-lg mb-4">
            Busco uma oportunidade na <strong>Netrin</strong>, onde posso aplicar minhas habilidades para criar soluções robustas e de alta qualidade.
          </p>

          <h2 className="text-3xl text-yellow-300 font-semibold mb-4">Tecnologias Utilizadas</h2>
          <ul className="list-disc pl-5 text-lg">
            <li><strong>Next.js</strong> (React) para a criação do Front-End interativo e escalável.</li>
            <li><strong>.NET 8</strong> para o desenvolvimento de Back-End com APIs seguras e eficientes.</li>
            <li><strong>SQL Server</strong> como sistema de gerenciamento de banco de dados.</li>
            <li><strong>FluentMigrator</strong>: Utilizado para a gestão de migrações de banco de dados e criação da tabela <strong>Produtos</strong>.</li>
            <li><strong>FluentValidation</strong>: Implementação de validações para garantir que dados como Nome (máx. 100 caracteres) e Preço (não nulo e maior que zero) sejam validados corretamente.</li>
            <li><strong>xUnit</strong>: Utilização de testes unitários para garantir a qualidade do código e cobrir operações do CRUD (Repository e Service), além das validações do FluentValidation.</li>
          </ul>
        </div>
      </main>

      <footer className="bg-gradient-to-r from-teal-600 to-blue-700 text-white py-8 mt-16">
        <div className="text-center">
          <p className="text-sm">&copy; 2024 Luigi Breda - Todos os direitos reservados.</p>
          <div className="flex justify-center gap-4 mt-4">
            <a
              className="text-white hover:text-gray-400"
              href="https://github.com/luigibreda"
              target="_blank"
              rel="noopener noreferrer"
            >
              GitHub
            </a>
            <a
              className="text-white hover:text-gray-400"
              href="https://linkedin.com/in/luigibreda"
              target="_blank"
              rel="noopener noreferrer"
            >
              LinkedIn
            </a>
          </div>
        </div>
      </footer>
    </div>
  );
}
