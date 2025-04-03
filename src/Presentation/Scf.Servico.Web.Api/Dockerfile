FROM docker-registry.dreads.bnb/ubi8/dotnet-60:6.0 AS build
WORKDIR /src

ENV PEM_DIR=/etc/pki/ca-trust/source/anchors/ca-bnb.pem
ENV PEM_URL=https://repo.dreads.bnb/repository/raw-tools-hosted/bnb/ca/ca-bnb.pem
ENV CAMINHO_PROJETO="src/Presentation/Scf.Servico.Web.Api/Scf.Servico.Web.Api.csproj"

USER 0
RUN curl -o ${PEM_DIR} -fsSLk ${PEM_URL} & update-ca-trust

COPY . '/src'
WORKDIR '/src'
USER 0
RUN dotnet restore "$CAMINHO_PROJETO" && \
    dotnet publish "$CAMINHO_PROJETO" -c Release -o /app/publish /p:UseAppHost=false

FROM docker-registry.dreads.bnb/ubi8/dotnet-60:6.0 AS deploy
WORKDIR /app

COPY --chown=default:root --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV PEM_DIR=/etc/pki/ca-trust/source/anchors/ca-bnb.pem
ENV PEM_URL=https://repo.dreads.bnb/repository/raw-tools-hosted/bnb/ca/ca-bnb.pem
ENV NOME_PROJETO="Scf.Servico.Web.Api.dll"
ENV TZ=America/Fortaleza

USER 0
RUN curl -o ${PEM_DIR} -fsSLk ${PEM_URL} & update-ca-trust
RUN chgrp -R 0 . && \
    chmod -R g+rwX .
RUN echo $TZ > /etc/timezone
ENTRYPOINT "dotnet" $NOME_PROJETO