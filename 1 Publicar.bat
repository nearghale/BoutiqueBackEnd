@echo off
color 2

dotnet publish -c Release
echo --------------- Publicou o projeto na pasta publish -----


cd BoutiquePool/bin/Release/netcoreapp3.1/publish
echo -------------- Entrou na pasta Publish ----------------


docker build --rm -f "Dockerfile" -t "unieco:latest" .
echo -------------- Criou o container docker ---------------

