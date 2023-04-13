#Imagen base para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

#Copiar el código fuente
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

#Imagen base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

#Definir las variables
ENV SqlServerSettings__ConnectionString=Server=.;Database=BD_NET_BACKEND;User Id=sa;Password=PasswordO1.
ENV MongoDatabaseSettings__ConnectionString=mongodb://root:rootpassword@localhost:27017/admin

#Definir los puertos a exponer
# EXPOSE 5000
# EXPOSE 5001

#Indicar la ejecución de la aplicación
ENTRYPOINT [ "dotnet", "/app/user.backend.api.dll" ]

#docker build -t user_microservice:1.0 . 