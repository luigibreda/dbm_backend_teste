"use client";

import { useState, useEffect } from "react";
import { Button, Input, Spinner } from "@nextui-org/react";
import { toast } from "react-toastify";
import { useProdutos } from "@/hooks/useProdutos";
import type { Produto } from "@/interfaces/produto";
import Breadcrumbs from "@/components/Breadcrumbs";

export default function ProdutoPage({ params }: { params: { id: string } }) {
  const { produtos, editarProduto, loading } = useProdutos();
  const produtoId = parseInt(params.id);

  const [produto, setProduto] = useState<Produto | null>(null);
  const [produtoEditado, setProdutoEditado] = useState<Produto | null>(null);

  useEffect(() => {
    const encontrado = produtos.find((p) => p.id === produtoId);
    setProduto(encontrado || null);
  }, [produtos, produtoId]);

  useEffect(() => {
    if (produto) {
      setProdutoEditado(produto);
    }
  }, [produto]);

  const handleChange = (field: keyof Produto, value: string) => {
    setProdutoEditado((prevProduto) => {
      if (prevProduto) {
        return { ...prevProduto, [field]: value };
      }
      return prevProduto;
    });
  };

  const handleSave = () => {
    if (produtoEditado) {
      if (
        !produtoEditado.nome ||
        !produtoEditado.descricao ||
        !produtoEditado.preco
      ) {
        toast.error("Todos os campos são obrigatórios");
        return;
      }
      editarProduto(produtoEditado);
    }
  };

  if (loading) {
    return <Spinner />;
  }

  if (!produtoEditado) {
    return (
      <>
        <div className="max-w-7xl mx-auto p-6">
          <div className="text-center mb-8">
            <h1 className="text-4xl font-semibold">Editar Produtos</h1>
            <p className="text-lg text-gray-500">
              Edite as informações do produto
            </p>
          </div>

          <Breadcrumbs />

          <div className="mb-6">
          <h1 className="text-4xl font-semibold mt-40 text-center uppercase">Produto não encontrado</h1>
          <p className="text-lg text-gray-500 text-center">Id inválido ou produto excluído, tente novamente.</p>
          </div>
        </div>
      </>
    );
  }

  return (
    <div className="max-w-7xl mx-auto p-6">
      <div className="text-center mb-8">
        <h1 className="text-4xl font-semibold">Editar Produtos</h1>
        <p className="text-lg text-gray-500">Edite as informações do produto</p>
      </div>

      <Breadcrumbs />

      <div className="mb-6">
        <Input
          label="Nome"
          value={produtoEditado.nome}
          onChange={(e) => handleChange("nome", e.target.value)}
        />
      </div>
      <div className="mb-6">
        <Input
          label="Descrição"
          value={produtoEditado.descricao}
          onChange={(e) => handleChange("descricao", e.target.value)}
        />
      </div>
      <div className="mb-6">
        <Input
          label="Preço"
          value={produtoEditado.preco}
          onChange={(e) => handleChange("preco", e.target.value)}
        />
      </div>

      <Button color="primary" onClick={handleSave}>
        Salvar
      </Button>
    </div>
  );
}
