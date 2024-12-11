# Golden Raspberry Awards Api

Este repositório contem a implementação de uma REST Api que exibe dados dos indicados e vencedores da categoria **_Pior Filme do Golden Raspberry Awards_**.
Foi criada para exibir o produtor com o maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido.

## Execução do projeto

Baixe o projeto com o comando a seguir:

```
git clone https://github.com/lpliberato/golden-raspberry-awards-api.git
```

Entre na pasta do projeto e execute a comando:

```
dotnet restore
```

E logo após execute o projeto:

```
dotnet run --project ./src/Movie.Api/Movie.Api.csproj
```

## Execução dos testes

Para executar os testes entre na pasta dos testes:

```
cd tests/Movie.Api.Tests
```

E depois execute o comando a seguir:

```
dotnet test
```
