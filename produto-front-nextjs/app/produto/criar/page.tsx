"use client";

import Breadcrumbs from "@/components/Breadcrumbs";
import { Produto } from "@/interfaces/produto";
import { Button, Input } from "@nextui-org/react";
import { useState } from "react";

export default function CriarProdutoPage() {
  const [novoProduto, setNovoProduto] = useState<Produto>({
    id: 0,
    nome: "",
    descricao: "",
    preco: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNovoProduto({
      ...novoProduto,
      [name]: value,
    });
  };

  const handleSave = () => {
    console.log("Produto criado", novoProduto);
  };

  return (
    <div className="max-w-7xl mx-auto p-6">
      <div className="text-center mb-8">
        <h1 className="text-4xl font-semibold">Criar Produto</h1>
        <p className="text-lg text-gray-500">Edite as informações do produto</p>
      </div>

      <Breadcrumbs />

      <div className="mb-6">
        <Input
          label="Nome"
          name="nome"
          value={novoProduto.nome}
          onChange={handleChange}
        />
      </div>
      <div className="mb-6">
        <Input
          label="Descrição"
          name="descricao"
          value={novoProduto.descricao}
          onChange={handleChange}
        />
      </div>
      <div className="mb-6">
        <Input
          label="Preço"
          name="preco"
          value={novoProduto.preco}
          onChange={handleChange}
        />
      </div>

      <Button color="primary" onClick={handleSave}>
        Criar Produto
      </Button>
    </div>
  );
}
