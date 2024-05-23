FROM ubuntu


RUN apt update
RUN apt install dotnet-sdk-8.0 -y

WORKDIR /app

COPY . .


ENTRYPOINT dotnet run
