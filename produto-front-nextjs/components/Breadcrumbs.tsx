'use client';

import { usePathname } from 'next/navigation';
import { Breadcrumbs as NextBreadcrumbs, BreadcrumbItem } from '@nextui-org/react';

const Breadcrumbs = () => {
  const pathname = usePathname();

  console.log('Pathname Atual:', pathname);

  if (!pathname) {
    return null;
  }

  const pathParts = pathname.replace(/^\/+/, '').split('/').filter(Boolean);

  console.log('pathParts:', pathParts); // Debug para ver a parte capturada

  const getBreadcrumbItems = () => {
    const breadcrumbItems = [];

    breadcrumbItems.push(
      <BreadcrumbItem key="home" href="/">
        Home
      </BreadcrumbItem>
    );

    if (pathParts[0] === 'produto') {
      breadcrumbItems.push(
        <BreadcrumbItem key="produto" href="/produto">
          Produtos
        </BreadcrumbItem>
      );

      if (pathParts[1] === 'criar') {
        breadcrumbItems.push(
          <BreadcrumbItem key="criar" href="/produto/criar">
            Criar Produto
          </BreadcrumbItem>
        );
      } else if (pathParts.length === 2 && !isNaN(Number(pathParts[1]))) {
        breadcrumbItems.push(
          <BreadcrumbItem key={pathParts[1]} href={`/produto/${pathParts[1]}`}>
            Produto {pathParts[1]}
          </BreadcrumbItem>
        );
      }
    }

    return breadcrumbItems;
  };

  return (
    <NextBreadcrumbs radioGroup='lg' variant="solid" className='mb-3'>
      {getBreadcrumbItems()}
    </NextBreadcrumbs>
  );
};

export default Breadcrumbs;
