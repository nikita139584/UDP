FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
RUN dotnet new console -n App
WORKDIR /app/App
COPY Server/Server.cs Program.cs
RUN dotnet build
CMD ["dotnet", "run"]
