echo off

echo A INICIAR BUILD E TESTES DA APLICACAO

echo PROCESSO FINALIDADO

dotnet build api && dotnet build testes && dotnet test TESTES

exit
