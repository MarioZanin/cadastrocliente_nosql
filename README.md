# CadastroCliente_NoSQL
O projeto CadastroCliente_NoSQL  tem como objetivo primordial oferecer uma interface de usuário (UI) limpa, funcional e responsiva, capaz de interagir com uma API de backend para executar operações de CRUD na base de dados de clientes. A aplicação é modular, seguindo o padrão MVC, o que facilita a separação de preocupações e a manutenção.
## 1. Visão Geral e Propósito
O projeto **Cadastro Cliente MVC** serve como a **camada de Apresentação (Front-end)** para um sistema de gestão de clientes. Seu objetivo primordial é oferecer uma interface de usuário (UI) limpa, funcional e responsiva, capaz de interagir com uma API de backend para executar operações de **CRUD (Create, Read, Update, Delete)** na base de dados de clientes.
A aplicação é modular, seguindo o padrão MVC, o que facilita a separação de preocupações e a manutenção.
## 2. Stack Tecnológico Principal
### 2.1. Plataforma e Linguagem Base
| Categoria | Tecnologia | Uso Específico |
| :--- | :--- | :--- |
| **Plataforma** | ASP.NET Core | Framework de desenvolvimento web moderno da Microsoft. Responsável pela execução do pipeline de requisições e pelo ambiente MVC. |
| **Linguagem** | C# | Linguagem primária para toda a lógica de controle (Controllers), Modelos de dados e lógica de serviço. |
| **Arquitetura** | ASP.NET Core MVC | Padrão arquitetural que garante a separação entre Lógica de Negócio (Model), Fluxo (Controller) e Interface (View). |
### 2.2. Tecnologias de Frontend e Auxiliares
| Categoria | Tecnologia | Detalhes e Funções |
| :--- | :--- | :--- |
| **Views** | Razor (`.cshtml`) | Sintaxe de marcação do .NET para geração dinâmica de HTML. Usada para iterar sobre dados do C# e renderizar a UI. |
| **Estilização** | CSS3 (Customizado) | Estilização **própria e sem frameworks (Bootstrap/Tailwind)**. Toda a aparência é centralizada em `wwwroot/css/site.css`. |
| **Helpers** | Tag Helpers | Usados nas Views Razor (ex: `asp-action`, `asp-for`) para gerar elementos HTML dinamicamente e lidar com rotas MVC, validação e formulários. |
| **Scripts** | JavaScript/JQuery | Incluídos no layout principal, são usados para scripts de validação (como `_ValidationScriptsPartial`) e potencial interatividade futura. |
## 3. Configuração do Pipeline e Injeção de Dependência (`Program.cs`)
O arquivo `Program.cs` define o coração da aplicação, configurando serviços e o pipeline de middleware.
### 3.1. Configuração de Serviços (Injeção de Dependência - DI)
A DI é configurada para garantir a disponibilidade dos seguintes serviços (baseado em um projeto típico com MongoDB):
| Serviço/Configuração | Tipo de Injeção | Propósito |
| :--- | :--- | :--- |
| **Controllers e Views** | `AddControllersWithViews()` | Habilita o suporte ao padrão MVC completo, incluindo a renderização de Views Razor. |
| **Configuração MongoDB** | `Configure<MongoDbSettings>()` | Associa a seção `MongoDbSettings` do `appsettings.json` a uma classe de configuração. |
| **Serviço de Cliente** | `AddSingleton<ClienteService>()` | Registra a classe de serviço que abstrai o acesso ao banco de dados (CRUD). Instanciada como **Singleton**. |
| **Swagger** | `AddSwaggerGen()` | Configuração para gerar a documentação interativa da API de backend (se hospedada no mesmo projeto). |
### 3.2. Configuração do Pipeline (Middleware)
O pipeline de requisição é definido na seguinte ordem (resumida):
1.  **Ambiente:** Habilita `UseSwagger()` (Dev/Debug).
2.  **HTTPS:** `UseHttpsRedirection()`.
3.  **Arquivos Estáticos:** `UseStaticFiles()` (crucial para carregar `site.css` e scripts).
4.  **Roteamento:** `UseRouting()`.
5.  **Autorização:** `UseAuthorization()`.
6.  **Mapeamento MVC (Padrão):** `MapControllerRoute()` com o padrão: **`{controller=Home}/{action=Index}/{id?}`**.
## 4. Estrutura de Arquivos e Views
### 4.1. Estrutura Mestra de Layout
| Caminho | Nome da View | Componentes Chave | Função |
| :--- | :--- | :--- | :--- |
| `/Views/Shared/` | `_Layout.cshtml` | `<header>`, `<nav>`, `<footer>` | **Template Principal.** Define a estrutura de navegação, inclui o `site.css` e os scripts base. |
| `/Views/` | `_ViewImports.cshtml` | `@using`, `@addTagHelper`, `Layout = "_Layout"` | Garante que todas as Views herdem o `_Layout.cshtml` e tenham acesso a Tag Helpers. |
### 4.2. Views de CRUD do Cliente
| Caminho | Nome da View | Propósito | Destaques de Estilização |
| :--- | :--- | :--- | :--- |
| `/Views/Clientes/` | `Index.cshtml` | Listagem de todos os clientes. | Tabela estilizada (classe `.table`) com ações em botões `.btn-sm`. |
| `/Views/Clientes/` | `Create.cshtml` | Formulário para criação. | Inputs com classe `.form-control`. Botão Salvar (`.btn-success`). |
| `/Views/Clientes/` | `Edit.cshtml` | Formulário para edição. | Campo `Id` oculto (`<input type="hidden">`). Botão Salvar Alterações (`.btn-info`). |
| `/Views/Clientes/` | `Details.cshtml` | Exibição de detalhes. | Usa lista de detalhes (`<dl>`/`<dt>`/`<dd>`) para formatação limpa. |
| `/Views/Clientes/` | `Delete.cshtml` | Confirmação de exclusão. | Exibe os detalhes e botão de exclusão perigoso (`.btn-danger`). |
## 5. Estilização (Customizada - `site.css`)
A aplicação adota um design personalizado, garantindo consistência visual em todos os componentes.
### 5.1. Layout e Responsividade
* **Corpo (`body`):** Fundo suave (`#f7f9fc`). Margem inferior ajustada para `40px` (`margin-bottom: 40px;`) para um rodapé discreto.
* **Contêiner (`.container`):** Centraliza o conteúdo com uma largura máxima de `1200px`.
* **Títulos (`h1`):** Azuis (`#007bff`) com linha divisória para estruturar a página.
### 5.2. Componentes de Navegação
| Componente | Estilo Principal | Destaque |
| :--- | :--- | :--- |
| **Navbar** | Fundo escuro (`#343a40`), altura reduzida (`padding: 0.5rem 1rem`). | Links com transição de cor para branco (`#ffffff`) ao passar o mouse. |
| **Rodapé (`.footer`)** | Fundo branco, altura reduzida (`line-height: 40px`). | Posicionado absolutamente na parte inferior da página (`position: absolute; bottom: 0;`). |
### 5.3. Sistema de Botões e Cores
O `site.css` define cores claras e específicas para as ações do CRUD:
| Ação Semântica | Classe CSS | Cor | Uso Típico |
| :--- | :--- | :--- | :--- |
| **Criar / Salvar** | `.btn-success` | Verde | Novo Cadastro, Salvar. |
| **Editar** | `.btn-info` | Ciano/Azul Claro | Editar, Salvar Alterações. |
| **Navegação/Secundária** | `.btn-secondary` | Cinza | Voltar, Detalhes. |
| **Excluir** | `.btn-danger` | Vermelho | Confirmação de Exclusão. |
### 5.4. Formulários e Tabelas
* **Inputs:** Classe `.form-control` para padronizar aparência, largura total e transição de foco suave.
* **Validação:** Mensagens de erro em vermelho (`.text-danger`).
* **Tabela (`.table`):** Cabeçalho cinza, fundo branco, com sombra (`box-shadow`) e destaque de linha ao passar o mouse.
## 6. Fluxo de Dados e Relacionamentos
O projeto MVC é um **cliente** da API. Seu relacionamento principal é via HTTP:
1.  **Controller (Ex: `ClientesController`):** Orquestra a operação.
2.  **Serviço (`ClienteService`):** Contém a lógica de comunicação (ex: usa `HttpClient`) para enviar requisições (GET, POST, PUT, DELETE) para os endpoints da API de backend.
3.  **Modelo (`Cliente.cs`):** O objeto de dados usado para serialização/deserialização entre o Controller e a API (JSON).
4.  **View (Ex: `Index.cshtml`):** Exibe os dados retornados e formatados pelo Controller.
**Objetivo:** Manter a lógica de UI separada da lógica de dados, garantindo que a aplicação seja escalável e fácil de manter.
