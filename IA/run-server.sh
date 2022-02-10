echo off

echo ##### ATUALIZAR APLICAÇÃO #####

  git pull

echo ##### A ATRIBUIR PERMISSÕES DE EXECUÇÃO #####
chmod 777 before-commit.sh && chmod 777 build-all.sh && chmod 777 run-server.sh && chmod 777 connect-to-server.sh && chmod 777 build-all.sh && chmod 777 run.sh && chmod 777 test.sh


echo ##### BUILD DO API #####

  dotnet build


echo ##### RUN API #####

  dotnet run --launch-profile "IAServer"
