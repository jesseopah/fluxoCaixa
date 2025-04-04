# fluxoCaixa
Teste para o banco carrefour

Com instruções claras de como a aplicação funciona, e como rodar localmente:
1.	Instalação do SQL Server Express Edition;
2.	Criar o Banco de Dados DBFLUXO1. Execute o arquivo Script1.sql que está na raiz do projeto.
3.	Executar o Script para a criação dos objetos;
4.	Instalar o Visual Studio 2022;
5.	Abrir o projeto da API;
6.	Configurar o arquivo appsettings.json:

"ConnectionStrings": {
   // Defina a string de conexão
   "Context": "Server=localhost\\SQLEXPRESS;Database=DBFLUXO1;Trusted_Connection=True;"
 },
 
7.	Executar o projeto da API;
8.	Testes diretos na API com o swagger;
9.	Configurar o AWS API Gateway com as rotas /lançamento e /saldo
10.	Efetuar os testes no AWS Gateway e no Postman;
