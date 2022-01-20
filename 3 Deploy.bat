@echo off
color 3

cd BoutiquePool/bin/Release/netcoreapp3.1/publish
echo -------------- Entrou na pasta Publish ----------------

heroku container:release web -a unieco
pause
echo -------------- DEPLOY FEITO COM SUCESSO ---------------

pause

