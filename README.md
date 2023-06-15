# ProjetoLeituraDeArquivos
Projeto leitura de arquivos horas de funcionários em csv e converter para json

O projeto Ponto2Pagamento é uma aplicação em .NET 6.0 desenvolvida como um projeto console. Ele tem como objetivo receber um arquivo CSV contendo o fechamento do ponto de uma empresa e gerar um arquivo JSON com a ordem de pagamento dos funcionários.

# Funcionalidades
. Leitura de arquivo CSV contendo o fechamento do ponto da empresa.

. Processamento dos dados para calcular a ordem de pagamento de cada funcionário.

. Geração de um arquivo JSON contendo as informações da ordem de pagamento.

# Utilização
Quando rodar o projeto ele ira pedir a informação do caminho da pasta que contém os arquivos em CSV.
Igual no exemplo a baixo.
![image](https://github.com/FerreiraVGabriel/ProjetoLeituraDeArquivos/assets/46385099/8fa1506f-9aed-40e2-992d-75185f5f4d44)
Obs: existe um validador para caminhos validos e um validador para nome de pasta inválida.

A pasta selecionada no exemplo a cima
![image](https://github.com/FerreiraVGabriel/ProjetoLeituraDeArquivos/assets/46385099/c76f1eb2-4efa-4257-90b2-a1cc3f1441bf)
Obs: Note que no exemplo possue outros arquivos de outros tipos e algumas pastas, so serão lido os arquivos csv.

O arquivo do tipo json tera um retorno igual na imagem a baixo

![image](https://github.com/FerreiraVGabriel/ProjetoLeituraDeArquivos/assets/46385099/977bdc75-977d-4c93-b664-5b062dfd73c0)

# Considerações
. No sistema fiz algumas modificações no retorno do json, pois na minha avaliação ficou melhor para fazer o retorno e mais explicativo para
  o usuário final
.  Quando vem com as duas datas (data de entrada e data de saída como nula) é considerado um dia de falta
. O sistema precisa ser preenchido todos os dias de semana, o sistema verifica quando é fim de semana 
. Caso do fim de semana seja preenchido é considerado como hora extra



