pipeline {
     agent { label "MASTER" 
	 }
    stages {
        stage("Definição de Ambiente de Publicação") {
            options {
                timeout(time: 60, unit: "SECONDS")
            }

            steps {
                script {
                    Exception caughtException = null

                    catchError(buildResult: 'SUCCESS', stageResult: 'ABORTED') { 
                        try {
							
							if (getProperty("BRANCH_STREAM").contains("RELEASE")) {
                             
                            echo "Default is: ${env.HML_PUBLISH}"
                                
							env.HML_PUBLISH = input(
									message: 'Publicar em Homologação?',
									parameters: [
											[$class: 'ChoiceParameterDefinition',
											 choices: ['YES','NO'].join('\n'),
											 name: 'Opções:',
											 description: 'Informe se deseja que a aplicação também seja distribuida para o ambiente de homologação']
									])
									
							echo "The answer is: ${env.HML_PUBLISH}"
							}
							else
							{
								env.HML_PUBLISH = 'NO'
							}
							
							echo "Final value is: ${env.HML_PUBLISH}"		
							
							
                        } catch (org.jenkinsci.plugins.workflow.steps.FlowInterruptedException e) {
                            error "Caught ${e.toString()}" 
                        } catch (Throwable e) {
                            caughtException = e
                        }
                    }

                    if (caughtException) {
                        error caughtException.message
                    }
                }
            }
        }
		
		stage('Checkout do Ambiente') {
            steps {
                echo "$BRANCH_STREAM"
                echo "Ajustando valores de propriedades"
                script {
                    currentBuild.displayName = "#${currentBuild.id} - $BRANCH_STREAM"
                    //configura diretório com valores da Build Definition RTC
                    env.setProperty("JOB_DIRETORIO", getProperty("team.scm.fetchDestination"))
                    //configura caminho da aplicacao
                    env.setProperty("JOB_CAMINHO_APLICACAO", getProperty("JOB_DIRETORIO") + "/" + getProperty("JOB_COMPONENTE"))
                    //adiciona pasta da aplicação .NET ou projeto java caso exista
                    if (getProperty("JOB_PASTA_APLICACAO") != "") {
                        setProperty("JOB_CAMINHO_APLICACAO", getProperty("JOB_CAMINHO_APLICACAO") + "/" + getProperty("JOB_PASTA_APLICACAO"))
                    }
                    //Configura o AMBIENTE
                    env.AMBIENTE = "desenvolvimento"
                    if (getProperty("BRANCH_STREAM").contains("RELEASE")) {
                        env.AMBIENTE = "release"
                    }
                }
                //script
                script {
                    echo "definindo aplicacao"
                    setProperty("NEXUS_IMAGE_NAME", getProperty("PROJECT_AREA") + "/" + getProperty("APLICACAO"));
                    setProperty("GIT_REPO_NAME", getProperty("APLICACAO"));
                    setProperty("PIPELINE_FASE", "PREPARACAO_AMBIENTE");
                }
                // baixa artefatos do RTC
                echo "RTC Checkout"

                // baixa artefatos do RTC
                checkout([
                    $class: 'RTCScm',
                    avoidUsingToolkit: false,
                    buildType: [
                        buildStream: "$BRANCH_STREAM",
                        customizedSnapshotName: "$JOB_COMPONENTE $BUILD_TIMESTAMP (Construção - $BRANCH_STREAM - #${currentBuild.id})",
                        overrideDefaultSnapshotName: true,
                        buildSnapshotContext: [
                            snapshotOwnerType: 'none',
                            processAreaOfOwningStream: "$PROJECT_AREA",
                            owningStream: "$BRANCH_STREAM"
                        ],
                        currentSnapshotOwnerType: 'none',
                        value: 'buildStream',
                        processArea: "$PROJECT_AREA",
                        buildStream: "$BRANCH_STREAM",
                        loadDirectory: "$JOB_DIRETORIO",
                        clearLoadDirectory: true,
                        loadPolicy: 'useComponentLoadConfig',
                        createFoldersForComponents: true,
                        componentLoadConfig: 'excludeSomeComponents',
                        componentsToExclude: ''
                    ],
                    timeout: 480
                ])

                // verifica a versão do aplicativo
                echo "Verificando Versão do Aplicativo"
                script {
                    def version = sh([returnStdout: true, script: 'sed -n "s/.*<Version>\\([^<]*\\)<\\/Version>.*/\\1/p"  ./' + getProperty("PROJECT_AREA") + '/' + getProperty("JOB_COMPONENTE") + '/' + getProperty("JOB_CAMINHO_CSPROJ")])
                    if (version == "") {
                        version = "1.0.0"
                    }
                    setProperty("VERSAO", version.trim());
                    echo getProperty("APLICACAO")
                }
            }
        }
		
		stage('Testes Unitários e Qualidade de Código') {
            steps {
                //build docker 
				//echo "Pulei os testes unitarios devido erro do ambiente em DEV \n"
                construirImagemTeste(getProperty("NEXUS_IMAGE_NAME"), getProperty("VERSAO"), getProperty("BUILD_NUMBER"))
            }
        }
		
        stage('Gerar Imagem Nexus') {
            steps {
                script {
                    currentBuild.displayName = "#${currentBuild.id} - ${VERSAO}-${currentBuild.id} - $BRANCH_STREAM"
                    env.setProperty('IMAGE_VERSION', getProperty('VERSAO') + '-' + getProperty('BUILD_NUMBER'))
                    echo "Gerando a imagem $NEXUS_IMAGE_NAME com a versão $VERSAO-$currentBuild.id \n"
                }
                construirImagem(getProperty("NEXUS_IMAGE_NAME"), getProperty("VERSAO"), getProperty("BUILD_NUMBER"))
            }
        }
		
        stage('Deploy Servidor Desenvolvimento') {
            when {
                environment name: 'AMBIENTE', value: 'desenvolvimento'
            }
            steps {
                //publicando no openshift				
                deployServidorDREADS(getProperty("VERSAO"), getProperty("BUILD_NUMBER"), getProperty("GIT_REPO_NAME"), 'desenvolvimento')
            }
            post {
                success {
                    setProperty("PIPELINE_FASE", "DEPLOY-DEV");
                }
            }
        }
		
        stage('Deploy Servidor Teste') {
            when {
                environment name: 'AMBIENTE', value: 'release'
            }
            steps {
                //publicando no openshift
                deployServidorDREADS(getProperty("VERSAO"), getProperty("BUILD_NUMBER"), getProperty("GIT_REPO_NAME"), 'testes-release')
            }
            post {
                success {
                    setProperty("PIPELINE_FASE", "DEPLOY-TST");
                    echo "Publicado em TESTE com sucesso"
                }
            }
        }
		
        stage('Deploy Servidor Homologação') {
            when {
                environment name: 'HML_PUBLISH', value: 'YES'
            }
            steps {
				echo "The answer was: ${env.HML_PUBLISH}"
				
                //publicando no openshift
                deployServidorCAPGV(getProperty("VERSAO"), getProperty("BUILD_NUMBER"), getProperty("GIT_REPO_NAME"), 'release')
            }
            post {
                success {
                    setProperty("PIPELINE_FASE", "DEPLOY-HML");
                    echo "Publicado em HOMOLOGAÇÃO com sucesso"
                }
            }
        }		
    }
}

// Gerar uma imagem para execução dos testes unitários e inspeção de código
def construirImagemTeste(tag, versao, build) {
    echo getProperty("JOB_CAMINHO_APLICACAO")
    dir("../$env.JOB_BASE_NAME/$JOB_CAMINHO_APLICACAO") {
        echo "Construindo imagem e executando teste unitário"
        echo getProperty("JOB_CAMINHO_DOCKERFILE_TESTE")
        image = docker.build("qualitygate" + "-" + tag.toLowerCase() + ":" + versao + "-" + build, "--cpuset-cpus 0,1 --force-rm  -f ./" + getProperty("JOB_CAMINHO_DOCKERFILE_TESTE") + " ./ --build-arg PROJECT_AREA=" + getProperty("PROJECT_AREA") +
            " --build-arg BUILD_NUMBER=" + getProperty("BUILD_NUMBER") + " --build-arg SNAPSHOTID=" + getProperty("team_scm_snapshotUUID") + " --build-arg STREAM=" + getProperty("BRANCH_STREAM"))
    }
}

// Gerar uma imagem e publicar no Nexus
def construirImagem(tag, versao, build) {
    dir("../$env.JOB_BASE_NAME/$JOB_CAMINHO_APLICACAO") {
        echo "Registrando o docker no ambiente $ambiente"
        docker.withRegistry('https://ocp-registry.dreads.bnb', '26991b27-10da-47b2-bdc4-e7b6ae67dd7c') {
            echo "Construindo imagem"
            // E preciso transformar isto em parametros para que seja alterado em cada projeto
            if (getProperty("BRANCH_STREAM").contains("RELEASE")) {
                perimetro = 'release/'
            } else {
                perimetro = 'dev/'
            }
            image = docker.build(perimetro + tag.toLowerCase() + ":" + versao + "-" + build, "--cpuset-cpus 0,1 --force-rm  -f ./" + getProperty("JOB_CAMINHO_DOCKERFILE") + " ./")
            echo "Publicado no Nexus"
            image.push()
            image.push('latest')
        }
    }
}

//Publicar aplicacao no OpenShift do DREADS
def deployServidorDREADS(versao, build, repositorio, branch) {
    script {
        // plugin de execução remota
        def handle = triggerRemoteJob(
            remoteJenkinsName: 'jenkins-pipelines.tst.ocp.dreads.bnb',
            job: 'Dreads-Pipeline',
            parameters: 'tag_version=' + versao + '-' + build + '\ngit_branch=' + branch + '\ngit_repo_name=' + repositorio + '-config' + '\ngit_repo_url=https://gitlab.dreads.bnb/openshift/' + repositorio + '-config.git'
        )
        // Get information from the handle
        def status = handle.getBuildStatus()
        def buildUrl = handle.getBuildUrl()
        echo buildUrl.toString() + " finished with " + status.toString()
        if (branch.contains("desenvolvimento")) {
            echo "https://" + repositorio + ".dev.ocp.dreads.bnb"
        } else {
            echo "https://" + repositorio + ".tst.ocp.dreads.bnb"
        }
    }
}

//Publicar aplicacao no OpenShift do CAPGV
def deployServidorCAPGV(versao, build, repositorio, branch) {
    script {
        // plugin de execução remota
        def handle = triggerRemoteJob(
            remoteJenkinsName: 'jenkins.hml.ocp.capgv.intra.bnb',
            job: 'Capgv-Pipeline',
            parameters: 'tag_version=' + versao + '-' + build + '\ngit_branch=' + branch + '\ngit_repo_name=' + repositorio + '-config' + '\ngit_repo_url=https://s1gitp01.capgv.intra.bnb/openshift/' + repositorio + '-config.git'
        )
        // Get information from the handle
        def status = handle.getBuildStatus()
        def buildUrl = handle.getBuildUrl()
        echo buildUrl.toString() + " finished with " + status.toString()
        //montando link para referência de url
        echo "https://" + repositorio + ".hml.ocp.capgv.intra.bnb/"
    }
}