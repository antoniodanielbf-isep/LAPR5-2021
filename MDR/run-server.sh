echo off

echo ##### ATUALIZAR APLICAÇÃO #####

  git pull

echo ##### A ATRIBUIR PERMISSÕES DE EXECUÇÃO #####

chmod 777 before-commit.sh && chmod 777 atualizar_base_de_dados.sh && chmod 777 build-all.sh && chmod 777 run-server.sh && chmod 777 connect-to-server.sh && chmod 777 run.sh && chmod 777 test.sh 

cd API

  chmod 777 build.sh
  chmod 777 run.sh
  chmod 777 database-update.sh

cd ..

cd TESTES

  chmod 777 build.sh
  chmod 777 run.sh

cd ..

echo ##### BUILD DO API #####

  cd API
    dotnet build
  cd ..

echo ##### BUILD DOS TESTES #####

  cd TESTES
      dotnet build
  cd ..


echo ##### RUN AOS TESTES #####

  cd TESTES
      dotnet test
  cd ..


echo ##### RUN API #####

  cd API
    dotnet run --launch-profile "MDRServer"
