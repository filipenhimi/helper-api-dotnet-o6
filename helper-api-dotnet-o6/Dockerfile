# Etapa 1: Usar uma imagem base do SDK do .NET 7 para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copiar o arquivo de projeto (.csproj) e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todos os outros arquivos e compilar o projeto
COPY . ./
RUN dotnet build -c Release -o out

# Publicar a aplicação para produção
RUN dotnet publish -c Release -o out

# Etapa 2: Usar uma imagem base mais leve do .NET runtime para rodar o aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copiar os arquivos publicados para o contêiner
COPY --from=build-env /app/out .

# Expor a porta 5156 
EXPOSE 5156

# Definir o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "helper-api-dotnet-o6.dll"]