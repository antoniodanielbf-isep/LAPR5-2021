echo off

echo A INICIAR A ATUALIZACAO DA BASE DE DADOS

cd API
database-update.sh
cd ..

echo ATUALIZACAO DA BASE DE DADOS CONCLUIDA!

exit