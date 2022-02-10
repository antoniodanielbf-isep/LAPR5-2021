echo off

echo A INICIAR A ATUALIZACAO DA BASE DE DADOS

cd API
database-update.bat
cd ..

echo ATUALIZACAO DA BASE DE DADOS CONCLUIDA!

exit