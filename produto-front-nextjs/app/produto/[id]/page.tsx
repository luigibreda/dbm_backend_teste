'use client';

import { useState, useEffect } from "react";
import { Button, Input, Spinner } from "@nextui-org/react";
import Breadcrumbs from "@/components/Breadcrumbs";

interface Produto {
  id: number;
  nome: string;
  descricao: string;
  preco: string;
}

export default function ProdutoPage({ params }: { params: { id: string } }) {
  const [produto, setProduto] = useState<Produto | null>(null);
  const [loading, setLoading] = useState<boolean>(true); 

  useEffect(() => {
    const { id } = params;

    const produtosMock = [
      {
        id: 1,
        nome: "Produto 1",
        descricao: "Descrição do Produto 1",
        preco: "R$ 50,00",
      },
      {
        id: 2,
        nome: "Produto 2",
        descricao: "Descrição do Produto 2",
        preco: "R$ 100,00",
      },
      {
        id: 3,
        nome: "Produto 3",
        descricao: "Descrição do Produto 3",
        preco: "R$ 150,00",
      },
    ];

    const produtoEncontrado = produtosMock.find(
      (prod) => prod.id === parseInt(id)
    );

    setTimeout(() => {
      if (produtoEncontrado) {
        setProduto(produtoEncontrado);
      } else {
        setProduto(null);
      }
      setLoading(false); 
    }, 1000); 
  }, [params]);

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <Spinner/>
      </div>
    ); 
  }

  if (!produto) {
    return <p>Produto não encontrado!</p>;
  }

  const handleChange = (field: keyof Produto, value: string) => {
    setProduto((prevProduto) => {
      if (prevProduto) {
        return { ...prevProduto, [field]: value };
      }
      return prevProduto;
    });
  };

  return (
    <div className="max-w-7xl mx-auto p-6">
      <div className="text-center mb-8">
        <h1 className="text-4xl font-semibold">Editar Produtos</h1>
        <p className="text-lg text-gray-500">
          Edite as informações do produto
        </p>
      </div>

      <Breadcrumbs />

      <div className="mb-6">
        <Input
          label="Nome"
          value={produto.nome}
          onChange={(e) => handleChange("nome", e.target.value)}
        />
      </div>
      <div className="mb-6">
        <Input
          label="Descrição"
          value={produto.descricao}
          onChange={(e) => handleChange("descricao", e.target.value)}
        />
      </div>
      <div className="mb-6">
        <Input
          label="Preço"
          value={produto.preco}
          onChange={(e) => handleChange("preco", e.target.value)}
        />
      </div>

      <Button
        color="primary"
        onPress={() => console.log("Salvar produto", produto)}
      >
        Salvar
      </Button>
    </div>
  );
}
