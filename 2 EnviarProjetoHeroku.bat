@echo off
color 3

cd BoutiquePool/bin/Release/netcoreapp3.1/publish
echo -------------- Entrou na pasta Publish ----------------

heroku container:push web -a unieco
echo -------------- Enviou o container para o heroku -------

