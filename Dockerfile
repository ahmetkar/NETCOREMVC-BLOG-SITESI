
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY BlogProjesi.sln ./
COPY Blog.Core/Blog.Core.csproj Blog.Core/
COPY Blog.Data/Blog.Data.csproj Blog.Data/
COPY Blog.Entity/Blog.Entity.csproj Blog.Entity/
COPY Blog.Service/Blog.Service.csproj Blog.Service/
COPY Blog.WebAPI/Blog.WebAPI.csproj Blog.WebAPI/


RUN dotnet restore BlogProjesi.sln


COPY Blog.Core/ Blog.Core/
COPY Blog.Data/ Blog.Data/
COPY Blog.Entity/ Blog.Entity/
COPY Blog.Service/ Blog.Service/
COPY Blog.WebAPI/ Blog.WebAPI/


WORKDIR /src/Blog.WebAPI
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


ENV ASPNETCORE_ENVIRONMENT=Production

# Publish aşamasında üretilen dosyaları çalışma dizinine kopyalıyoruz
COPY --from=build /app/publish .


COPY Blog.WebAPI/Properties/google.json ./Properties/google.json

ENTRYPOINT ["dotnet", "Blog.WebAPI.dll"]

