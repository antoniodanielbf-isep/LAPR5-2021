echo off

echo A INICIAR OS PROCEDIMENTOS ANTERIORES AO COMMIT

git pull && dotnet build API && dotnet build TESTES && dotnet test TESTES

echo PRONTO PARA EFETUAR O COMMIT

exit