# Projeto AnimalManager

O **AnimalManager** é uma aplicação C# para gerenciamento de informações sobre animais, utilizando **SQLite** como banco de dados. O projeto permite listar, cadastrar, atualizar e deletar informações sobre animais.

## Funcionalidades

- Listar todos os animais cadastrados no banco de dados.
- Adicionar novos animais com informações como nome, idade, espécie e data de adoção.
- Atualizar informações de animais existentes.
- Deletar registros de animais do banco de dados.
- Banco de dados SQLite integrado e inicializado automaticamente.
- **Sistema de Logs**: As operações importantes e erros são registrados no console para monitoramento e diagnóstico.(PODE CONTER ERROS)
- **Conteinerização**: A aplicação foi conteinerizada usando Docker, permitindo a execução em um ambiente isolado e padronizado.(PODE CONTER ERROS)

## Estrutura do Projeto

### `Animal.cs`
- **Objetivo**: Define a classe `Animal`, que representa o modelo de dados utilizado na aplicação.
- **Propriedades**:
  - `Id`: Identificador único do animal.
  - `Nome`: Nome do animal.
  - `Idade`: Idade do animal.
  - `Especie`: Espécie do animal.
  - `DataAdocao`: Data em que o animal foi adotado (opcional).

### `AnimalService.cs`
- **Objetivo**: Fornece métodos para manipulação dos dados no banco de dados.
- **Métodos**:
  - `ListarAnimais()`: Retorna uma lista de todos os animais cadastrados.
  - `AddAnimal(Animal animal)`: Adiciona um novo registro ao banco de dados.
  - `BuscarAnimalPorId(int id)`: Retorna as informações de um animal pelo seu ID.
  - `AtualizarAnimal(Animal animal)`: Atualiza informações de um registro existente.
  - `DeletarAnimal(int id)`: Remove um registro do banco de dados com base no ID.

### `DatabaseService.cs`
- **Objetivo**: Gerencia a conexão e a inicialização do banco de dados SQLite.
- **Métodos**:
  - `InitializeDatabase()`: Cria a tabela `Animal` caso ela não exista.
  - `GetConnection()`: Retorna uma conexão ativa com o banco de dados.

### `Programa.cs`
- **Objetivo**: Fornece a interface principal do usuário, permitindo interação com o sistema.
- **Funcionalidades**:
  - Menu interativo com opções para listar, cadastrar, atualizar e deletar animais.
  - Validações de entrada para evitar erros no cadastro e atualização.
  - Tratamento de erros e mensagens de retorno ao usuário.
  - **Sistema de Logs**: O sistema de logs foi integrado para registrar operações importantes e erros.

## Pré-requisitos

- **.NET SDK**: Versão 8.0 ou superior.
- **SQLite**: Integrado via pacotes NuGet.
- **Docker**: Caso queira rodar a aplicação em um contêiner, o Docker deve estar instalado.

## Configuração e Execução

### Executando o Projeto Localmente
1. Clone este repositório.
2. Restaure os pacotes NuGet com o comando:
    ```bash
    dotnet restore
    ```
3. Compile e execute o projeto com:
    ```bash
    dotnet run
    ```

### Executando o Projeto com Docker
1. **Build da Imagem Docker**: No diretório do projeto, execute:
    ```bash
    docker build -t animal-manager .
    ```
2. **Executando o Contêiner**: Após o build, rode a aplicação em um contêiner:
    ```bash
    docker run --rm -it animal-manager
    ```

## Dependências

As seguintes dependências são usadas no projeto:

- **Dapper**: Para simplificar consultas ao banco de dados.
- **System.Data.SQLite.Core**: Para integração com o banco de dados SQLite.
- **Microsoft.Extensions.Logging.Console**: Para registrar logs no console.

## Estrutura do Banco de Dados

A tabela `Animal` possui os seguintes campos:

- `Id`: Inteiro, chave primária, gerado automaticamente.
- `Nome`: Texto, obrigatório.
- `Idade`: Inteiro, obrigatório.
- `Especie`: Texto, obrigatório.
- `DataAdocao`: Texto, opcional.

## Exemplo de Uso

### Adicionar um animal
O sistema solicitará as informações do animal, incluindo nome, idade, espécie e data de adoção. Caso a data não seja informada, será usada a data atual.

### Atualizar um animal
É possível alterar o nome, idade, espécie ou data de adoção de um animal existente, informando o ID.

### Listar animais
Todos os registros serão exibidos no formato:


### Deletar um animal
Informe o ID do animal a ser removido.

## Logs

- Todas as operações importantes (como cadastro, atualização, e exclusão de animais) são registradas no console.
- Em caso de erro, as mensagens de erro são capturadas e registradas para facilitar o diagnóstico.

## Licença

Este projeto está licenciado sob a Licença MIT.
