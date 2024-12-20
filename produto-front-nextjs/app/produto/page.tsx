'use client';

import { useEffect, useState } from 'react';
import { Table, Button, Spacer, TableHeader, TableColumn, TableBody, TableRow, TableCell, Input } from '@nextui-org/react';
import Link from 'next/link';
import Breadcrumbs from '@/components/Breadcrumbs';

interface Produto {
  id: number;
  nome: string;
  descricao: string;
  preco: string;
}

export default function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [filtro, setFiltro] = useState<string>('');

  useEffect(() => {
    const produtosMock = [
      { id: 1, nome: 'Produto 1', descricao: 'Descrição do Produto 1', preco: 'R$ 50,00' },
      { id: 2, nome: 'Produto 2', descricao: 'Descrição do Produto 2', preco: 'R$ 100,00' },
      { id: 3, nome: 'Produto 3', descricao: 'Descrição do Produto 3', preco: 'R$ 150,00' },
    ];

    setProdutos(produtosMock);

    // api.get('/produtos')
    //   .then((res) => setProdutos(res.data))
    //   .catch((err) => {
    //     console.error('Erro ao buscar produtos:', err);
    //     setProdutos(produtosMock); // Mock em caso de erro
    //   });
  }, []);

  const produtosFiltrados = produtos.filter((produto) =>
    produto.nome.toLowerCase().includes(filtro.toLowerCase())
  );

  return (
    (
      <div className="max-w-7xl mx-auto p-6">
      {/* Título e Subtítulo */}
      <div className="text-center mb-8">
        <h1 className="text-4xl font-semibold">Produtos</h1>
        <p className="text-lg text-gray-500">Gerencie os produtos da sua loja</p>
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

      {/* Tabela de Produtos */}
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
              <TableCell>
                <div style={{ display: 'flex', gap: '10px' }}>
                  <Link href={`/produto/${produto.id}`}>
                    <Button color="primary" size="sm">Editar</Button>
                  </Link>
                  <Button
                    variant="bordered"
                    size="sm"
                    color="danger"
                    onPress={() => console.log('Delete', produto.id)}                  >
                    Excluir
                  </Button>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  ));
}
