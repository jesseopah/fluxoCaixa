FROM docker-private.dreads.bnb/bnb/dotnet-60:6.0 

# Definição de argumentos que serão usados na construção
ARG STREAM
ARG SNAPSHOTID
ARG BUILD_NUMBER
ARG PROJECT_AREA
ARG JOB_COMPONENTE

ENV PEM_DIR=/etc/pki/ca-trust/source/anchors/ca-bnb.pem
ENV PEM_URL=https://repo.dreads.bnb/repository/raw-tools-hosted/bnb/ca/ca-bnb.pem
ENV CAMINHO_PROJETO="src/Presentation/Scf.Servico.Web.Api/Scf.Servico.Web.Api.csproj"

USER 0
# Configurações dos caminhos do projeto e arquivos relacionados
ENV ARQUIVO_PROJETO="Scf.Servico.Web.Api.csproj"
ENV CAMINHO_PROJETO="src/Presentation/Scf.Servico.Web.Api/"
ENV ARQUIVO_PROJETO_TESTE="Scf.Servico.Test.csproj"
ENV CAMINHO_PROJETO_TESTE="test/Scf.Servico.Test/"
ENV ARQUIVO_NUGET="nuget.config"

# Configurações para execução da análise estática de código no Sonar
ENV CHAVE_PROJETO_SONAR="S461_MGCAC_Servico_WEBAPI:Scf.Servico.Test:"
ENV LOGIN_SONAR="0a2b602a91a888b94eff76d9b9a53d1ac9165071"

# Cópia dos arquivos necessários para suas respectivas pastas
COPY $CAMINHO_PROJETO$ARQUIVO_PROJETO $CAMINHO_PROJETO
COPY $CAMINHO_PROJETO$ARQUIVO_NUGET $CAMINHO_PROJETO
COPY $CAMINHO_PROJETO_TESTE$ARQUIVO_PROJETO_TESTE $CAMINHO_PROJETO_TESTE

# Restauração do projeto de testes e suas dependências
RUN dotnet restore "$CAMINHO_PROJETO_TESTE$ARQUIVO_PROJETO_TESTE" --configfile "$CAMINHO_PROJETO$ARQUIVO_NUGET"
COPY . .
WORKDIR "$CAMINHO_PROJETO_TESTE"

# Execução do projeto de testes
RUN dotnet test "$ARQUIVO_PROJETO_TESTE" -c Release -o /app/build

# Execução do SonarScanner para avaliação do código
RUN dotnet sonarscanner begin /k:"$CHAVE_PROJETO_SONAR$STREAM" /d:sonar.host.url=https://sonarqube.tst.ocp.dreads.bnb/ /d:sonar.login="$LOGIN_SONAR" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.scanner.metadataFilePath=/tmp/report-task.txt
RUN dotnet-coverage collect 'dotnet test' -f xml -o 'coverage.xml'
RUN dotnet sonarscanner end /d:sonar.login="$LOGIN_SONAR"

# Exporta o relatório da análise e envia para o servidor
RUN cat /tmp/report-task.txt
RUN export TASKID=$(cat /tmp/report-task.txt | grep ceTaskId= | cut -d'=' -f2) && \
curl https://s491-informacoes-sonar-api.dev.ocp.dreads.bnb/avaliacao/$PROJECT_AREA/$TASKID/$BUILD_NUMBER/PIPE/$SNAPSHOTID