# Use a imagem base do .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho dentro do container
WORKDIR /src

# Copie o arquivo csproj e restore as dependências
COPY ["ProdutoAPI.csproj", "./"]
RUN dotnet restore "ProdutoAPI.csproj"

# Copie os demais arquivos do projeto para dentro do container
COPY . .

# Exclua os diretórios bin/obj para evitar problemas de permissões
RUN rm -rf /src/bin /src/obj

# Publique o aplicativo
RUN dotnet publish "ProdutoAPI.csproj" -c Release -o /app/publish

# Gerar a imagem final com o runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copie os arquivos publicados do container de build
COPY --from=build /app/publish .

# Configurar o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "ProdutoAPI.dll"]
