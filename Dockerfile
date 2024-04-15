# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
# WORKDIR /app
# EXPOSE 5000

# ENV ASPNETCORE_URLS=http://+:5000

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG configuration=Release
# WORKDIR /src
# COPY ["Project/anuglar_crud.csproj", "Project/"]
# RUN dotnet restore "Project\anuglar_crud.csproj"
# COPY . .
# WORKDIR "/src/Project"
# RUN dotnet build "anuglar_crud.csproj" -c $configuration -o /app/build

# FROM build AS publish
# ARG configuration=Release
# RUN dotnet publish "anuglar_crud.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "anuglar_crud.dll"]

FROM node:14 AS frontend
WORKDIR /app
COPY Project/ClientApp/package*.json ./
RUN npm install
COPY Project/ClientApp .
RUN npm run build -- --prod


# Use a .NET Core SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and the script to modify it
COPY Project/anuglar_crud.sln ./
COPY Project/change_sln.sh ./

# Run the script to modify the solution file
# RUN chmod +x change_sln.sh
# RUN ./change_sln.sh

# Copy the .csproj file and restore any dependencies
COPY Project/*.csproj ./

RUN dotnet restore  

# Copy the entire project and build it
COPY Project/ ./
RUN dotnet build -c Release -o out

# Expose the necessary port for the .NET Core application
EXPOSE 80

# Start the .NET Core application, which also serves the Angular frontend
ENTRYPOINT ["dotnet", "run", "--project", "anuglar_crud.csproj"]
