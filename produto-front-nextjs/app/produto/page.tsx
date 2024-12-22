"use client";

import {
  Button,
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Spinner,
  Input,
} from "@nextui-org/react";
import Link from "next/link";
import { useProdutos } from "@/hooks/useProdutos";
import Breadcrumbs from "@/components/Breadcrumbs";
import { useState } from "react";
import { FiEdit, FiTrash } from "react-icons/fi";

export default function ProdutosPage() {
  const { produtos, loading, deletarProduto } = useProdutos();
  const [filtro, setFiltro] = useState<string>("");

  if (loading) {
    return (
      <>
        <div className="max-w-7xl mx-auto p-6">
          <div className="text-center mb-8">
            <h1 className="text-4xl font-semibold">Produtos</h1>
            <p className="text-lg text-gray-500">
              Gerencie os produtos da sua loja
            </p>
          </div>

          <Breadcrumbs />

          <div className="mb-6 text-center">
            <Spinner />
          </div>
        </div>
      </>
    );
  }

  const produtosFiltrados = produtos.filter((produto) =>
    produto.nome.toLowerCase().includes(filtro.toLowerCase())
  );

  return (
    <div className="max-w-7xl mx-auto p-6">
      {/* Título e Subtítulo */}
      <div className="text-center mb-8">
        <h1 className="text-4xl font-semibold">Produtos</h1>
        <p className="text-lg text-gray-500">
          Gerencie os produtos da sua loja
        </p>
      </div>

      <Breadcrumbs />

      {/* Filtro de produtos */}
      <div className="flex justify-between items-center mb-6">
        <div className="w-full max-w-md">
          <Input
            placeholder="Buscar produto"
            value={filtro}
            onChange={(e) => setFiltro(e.target.value)}
            aria-label="Filtro de produtos"
          />
        </div>
        <div className="w-full max-w-xs ml-4">
          <Link href="/produto/criar">
            <Button color="primary" size="lg" fullWidth>
              Criar Produto
            </Button>
          </Link>
        </div>
      </div>

      <Table aria-label="Tabela de Produtos">
        <TableHeader>
          <TableColumn>ID</TableColumn>
          <TableColumn>Nome</TableColumn>
          <TableColumn>Descrição</TableColumn>
          <TableColumn>Preço</TableColumn>
          <TableColumn>Ações</TableColumn>
        </TableHeader>
        <TableBody>
          {produtosFiltrados.map((produto) => (
            <TableRow key={produto.id}>
              <TableCell>{produto.id}</TableCell>
              <TableCell>{produto.nome}</TableCell>
              <TableCell>{produto.descricao}</TableCell>
              <TableCell>{produto.preco}</TableCell>
              <TableCell className="flex space-x-4">
                <Link
                  href={`/produto/${produto.id}`}
                  aria-label="Editar Produto"
                >
                  <FiEdit className="text-blue-500 cursor-pointer" size={20} />
                </Link>
                <FiTrash
                  className="text-red-500 cursor-pointer"
                  size={20}
                  onClick={() => deletarProduto(produto.id)}
                  aria-label="Excluir Produto"
                />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
