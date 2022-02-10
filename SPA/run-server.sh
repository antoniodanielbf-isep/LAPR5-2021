echo off

echo ##### ATUALIZAR APLICAÇÃO #####

   git pull

echo ##### A ATRIBUIR PERMISSÕES DE EXECUÇÃO #####

   chmod 777 before-commit.sh &&  chmod 777 build-all.sh &&  chmod 777 run-server.sh && chmod 777 connect-to-server.sh && chmod 777 run.sh && chmod 777 nvm-config.sh


echo ##### EXPANDIR MEMÓRIA PARA A APLICAÇÃO #####
echo ##### BUILD & RUN VISUALIZACAO #####

node --max-old-space-size=8192 node_modules/@angular/cli/bin/ng serve --host 10.9.21.156
