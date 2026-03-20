# Biblioteca API - Desafio Técnico

Olá! Este projeto é uma **API de Gerenciamento de Biblioteca** desenvolvida para resolver um desafio técnico. O objetivo é gerenciar usuários, livros e empréstimos de forma organizada, segura e seguindo as melhores práticas de desenvolvimento.

---

## Tecnologias Utilizadas

- **.NET 9**: A versão mais recente do framework da Microsoft.
- **ASP.NET Core Web API**: Para criar os pontos de acesso (endpoints) da nossa aplicação.
- **Entity Framework Core**: Nosso "tradutor" entre o código C# e o banco de dados.
- **SQLite**: Um banco de dados leve e que não precisa de instalação complexa.
- **JWT (Json Web Token)**: Para garantir que apenas pessoas autorizadas acessem certas partes da API.
- **BCrypt**: Para esconder (hash) as senhas dos usuários com segurança.
- **Docker**: Para que qualquer pessoa consiga rodar o projeto sem quebrar a cabeça com configurações.

---

## Arquitetura

Utilizei uma estrutura **Clean Architecture**. A ideia é separar as responsabilidades para que o código não vire uma "bagunça":

1. **Domain**: O coração do sistema. Aqui ficam as regras de negócio puras e as validações principais.
2. **Application**: Aqui ficam os **Use Cases**. Cada pasta representa uma ação real: "Cadastrar Livro", "Realizar Empréstimo", etc. É o "cérebro" que coordena o fluxo.
3. **Infrastructure**: A parte "braçal". Aqui fica a conexão com o banco de dados, o serviço de tokens JWT e a criptografia.
4. **Presentation**: A porta de entrada da API. Aqui ficam os **Controllers**, que recebem as requisições da internet e devolvem as respostas.

---

## Requisitos do Desafio e Como Resolvi

Abaixo estão os pontos específicos solicitados no desafio e como cada um foi implementado:

### 1. Validar CPF do usuário
- **Requisito**: No cadastro de usuário, validar se o CPF já existe antes de cadastrar. Caso já exista, retornar erro.
- **Solução**: No `CriarUsuarioUseCase`, o sistema consulta o banco de dados através do repositório para verificar a existência do CPF. Se encontrado, uma `DomainException` é lançada com a mensagem de erro.

### 2. Validar ISBN do livro
- **Requisito**: O campo ISBN deve conter exatamente 13 dígitos numéricos.
- **Solução**: Criei um **Value Object** chamado `ISBN`. Ele utiliza expressões regulares para garantir que apenas números sejam aceitos e valida se a extensão é exatamente de 13 dígitos, além de aplicar o algoritmo oficial de validação de ISBN-13.

### 3. Ajuste de Multa
- **Requisito**: Gerando multa mesmo quando o empréstimo é devolvido no prazo.
- **Solução**: Corrigi o cálculo na `EmprestimoEntity`. Agora, o sistema subtrai a data atual da data prevista de devolução. Se o resultado for 0 ou negativo (dentro do prazo), a multa é zerada explicitamente.

### 4. Livro já emprestado
- **Requisito**: Impedir que um livro que já está emprestado e não devolvido seja emprestado novamente.
- **Solução**: Adicionei um campo `EmUso` na entidade `Livro`. Ao tentar criar um novo empréstimo, o `CriarEmprestimoUseCase` verifica esse status. Se o livro estiver em uso, retorna a mensagem: *"Este livro já está emprestado e ainda não foi devolvido."*

### 5. Listar todos os livros
- **Requisito**: Criar endpoint `GET /livro/listar` que retorne a lista completa.
- **Solução**: Implementei o endpoint no `LivrosController`. Ele chama o serviço de aplicação que busca todos os registros ativos no banco de dados e os formata em um JSON limpo para o usuário.

### 6. Bloqueio por Atraso Ativo
- **Requisito**: Impedir novo empréstimo se o usuário tiver algum livro em atraso.
- **Solução**: Criei o `AtrasoService`, que analisa todos os empréstimos do usuário. Se houver algum atraso, uma marcação `PossuiAtrasoAtivo` é salva no cadastro do usuário no banco de dados, e o novo empréstimo é bloqueado com a mensagem específica "Usuário com empréstimo em atraso não pode realizar novo empréstimo.".

### 7. Nova Regra de Multa Progressiva
- **Requisito**: Implementar lógica de multa (até 3 dias = R$ 2,00/dia; a partir do 4º dia = R$ 3,50/dia; limite de R$ 50,00).
- **Solução**: Refatorei o método `CalcularMulta` na `EmprestimoEntity`. Ele agora aplica os multiplicadores diferentes dependendo da quantidade de dias e utiliza `Math.Min` para garantir que o valor nunca ultrapasse os R$ 50,00 previstos.

### 8. Autenticação JWT (Bearer)
- **Requisito**: Proteger endpoints sensíveis (Cadastro de Livro, Empréstimos, Devoluções) com autenticação.
- **Solução**: Configurei a autenticação **JWT** no `Program.cs`. Endpoints administrativos exigem a role `Administrador`, enquanto endpoints de consulta (como listar livros) permitem acesso de qualquer usuário autenticado. Tokens inválidos recebem automaticamente erro `401 Unauthorized`.

---

## Como Rodar o Projeto

Se você tem o **Docker** instalado, basta rodar um comando na raiz da pasta:

```bash
docker-compose up --build
```

A API ficará disponível em `http://localhost:5000`. Você também pode acessar o **Swagger** em `/swagger` para testar os endpoints visualmente!

---