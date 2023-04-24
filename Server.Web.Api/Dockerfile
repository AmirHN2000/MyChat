FROM mcr.microsoft.com/dotnet/aspnet:7.0

RUN apt-get update && \
    apt-get install -y --no-install-recommends vim unzip

WORKDIR /app

COPY . /app

CMD ["dotnet", "Server.Web.Api.dll"]