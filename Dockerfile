# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia a solução e restaura
COPY ./*.sln ./
COPY ./ProdutoCrudSolution.Api/*.csproj ./ProdutoCrudSolution.Api/
COPY ./ProdutoCrudSolution.Domain/*.csproj ./ProdutoCrudSolution.Domain/
COPY ./ProdutoCrudSolution.Application/*.csproj ./ProdutoCrudSolution.Application/
COPY ./ProdutoCrudSolution.Infrastructure/*.csproj ./ProdutoCrudSolution.Infrastructure/
COPY ./ProdutoCrudSolution.Tests/*.csproj ./ProdutoCrudSolution.Tests/

RUN dotnet restore ./ProdutoCrudSolution.sln

# Copiar o restante do código
COPY . .
RUN dotnet publish ./ProdutoCrudSolution.Api/ProdutoCrudSolution.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProdutoCrudSolution.Api.dll"]
