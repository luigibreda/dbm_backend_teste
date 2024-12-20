# Imagem base para Node.js
FROM node:18-alpine AS build

# Defina o diretório de trabalho no container
WORKDIR /app

# Copie os arquivos do projeto
COPY . .

# Instale as dependências do projeto
RUN npm install --legacy-peer-deps

# Gere a build de produção do Next.js
RUN npm run build

# Exponha a porta para o Next.js
EXPOSE 3000

# Comando para rodar o Next.js
CMD ["npm", "start"]
