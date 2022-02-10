echo off
echo #### ATUALIZAR APLICAÇÃO #####

   git pull

echo ##### A ATRIBUIR PERMISSÕES DE EXECUÇÃO #####

   chmod 777 before-commit.sh &&  chmod 777 build-all.sh && chmod 777 run-server.sh && chmod 777 connect-to-server.sh && chmod 777 nvm-config.sh && chmod 777 run.sh && chmod 777 configurar-var-amb.sh


echo ##### BUILD & RUN VISUALIZACAO #####

   ./run.sh
