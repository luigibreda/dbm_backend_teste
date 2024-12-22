import type { Produto } from "@/interfaces/produto";
import { useState, useEffect } from "react";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const API_URL = "https://ca32bf839dce517de025.free.beeceptor.com/api/produtos/";

export function useProdutos() {
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // Função para carregar todos os produtos
  const carregarProdutos = async () => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(API_URL);
      const data = await response.json();
      setProdutos(data);
      toast.success("Produtos carregados com sucesso!");
    } catch (err) {
      setError("Erro ao carregar produtos");
      toast.error("Erro ao carregar produtos");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Função para criar um novo produto
  const criarProduto = async (novoProduto: Produto) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(API_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(novoProduto),
      });
      const data = await response.json();
      setProdutos((prev) => [...prev, data]);
      toast.success("Produto criado com sucesso!");
    } catch (err) {
      setError("Erro ao criar produto");
      toast.error("Erro ao criar produto");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Função para editar um produto
  const editarProduto = async (produtoAtualizado: Produto) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`${API_URL}${produtoAtualizado.id}/`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(produtoAtualizado),
      });
      const data = await response.json();
      setProdutos((prev) =>
        prev.map((produto) =>
          produto.id === data.id ? { ...produto, ...data } : produto
        )
      );
      toast.success("Produto editado com sucesso!");
    } catch (err) {
      setError("Erro ao editar produto");
      toast.error("Erro ao editar produto");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Função para deletar um produto
  const deletarProduto = async (id: number) => {
    setLoading(true);
    setError(null);

    try {
      await fetch(`${API_URL}${id}/`, {
        method: "DELETE",
      });
      setProdutos((prev) => prev.filter((produto) => produto.id !== id));
      toast.success("Produto excluído com sucesso!");
    } catch (err) {
      setError("Erro ao excluir produto");
      toast.error("Erro ao excluir produto");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarProdutos();
  }, []);

  return {
    produtos,
    loading,
    error,
    carregarProdutos,
    criarProduto,
    editarProduto,
    deletarProduto,
  };
}
