FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 80
                                                                                                                           
# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY BLL.Base/*.csproj ./BLL.Base/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.BLL.Base/*.csproj ./Contracts.BLL.Base/
COPY Cache/*.csproj ./Cache/
COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY Contracts.DAL.Base/*.csproj ./Contracts.DAL.Base/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY DAL.App.NoSQL/*.csproj ./DAL.App.NoSQL/

COPY DAL.Base/*.csproj ./DAL.Base/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/
COPY Domain/*.csproj ./Domain/                                                                                                                                                                                                                 
COPY Public.DTO/*.csproj ./Public.DTO/
COPY Extensions/*.csproj ./Extensions/
COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore
# copy everything else and build app
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY BLL.Base/. ./BLL.Base/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.BLL.Base/. ./Contracts.BLL.Base/
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY Contracts.DAL.Base/. ./Contracts.DAL.Base/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY DAL.App.NoSQL/. ./DAL.App.NoSQL/
COPY Cache/. ./Cache
COPY DAL.Base/. ./DAL.Base/
COPY DAL.Base.EF/. ./DAL.Base.EF/
COPY Domain/. ./Domain/
COPY Public.DTO/. ./Public.DTO/
COPY Extensions/. ./Extensions/
COPY WebApp/. ./WebApp/



WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out
 

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:MySqlServerConnection="Server=172.17.0.2; port=3306; database=distributed; user=root; password=myPass123; Persist Security Info=false; Connect Timeout=300"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
