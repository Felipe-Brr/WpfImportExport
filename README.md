# WpfImportExport

## Descrição

O projeto **WpfImportExport** é uma aplicação WPF que permite a importação e exportação de blocos PLC do TIA Portal V18 utilizando a API Siemens TIA Openness. A aplicação fornece uma interface gráfica para facilitar a interação com os arquivos de projeto e os blocos PLC.

## Estrutura do Projeto

- **Properties/AssemblyInfo.cs**: Contém informações gerais sobre o assembly.
- **Models/Projeto.cs**: Define a classe `Projeto` que gerencia as operações de importação e exportação de blocos PLC.
- **MainWindow.xaml.cs**: Define a lógica de interação para a janela principal da aplicação.
- **Views/Message.xaml.cs**: Define a lógica de interação para a página de mensagens.
- **Views/ExportarBloco.xaml.cs**: Define a lógica de interação para a página de exportação de blocos.

## Funcionalidades

- **Procurar Projeto**: Permite ao usuário selecionar um arquivo de projeto.
- **Abrir Projeto**: Abre o projeto selecionado utilizando a API Siemens.
- **Procurar Caminho de Exportação**: Permite ao usuário selecionar um diretório para exportar os blocos PLC.
- **Exportar Blocos**: Exporta os blocos PLC para o diretório selecionado.
- **Procurar Arquivo para Importar**: Permite ao usuário selecionar um arquivo para importar blocos PLC.
- **Importar Blocos**: Importa os blocos PLC do arquivo selecionado.

## Requisitos

- **.NET Framework 4.8**
- **C# 7.3**

## Como Executar

1. Clone o repositório.
2. Abra a solução no Visual Studio.
3. Compile a solução.
4. Execute o projeto.

## Estrutura de Classes

### Projeto

A classe `Projeto` gerencia as operações de importação e exportação de blocos PLC.


### ProjetoViewModel

A classe `ProjetoViewModel` gerencia a lógica de apresentação para a interface do usuário.


## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.
