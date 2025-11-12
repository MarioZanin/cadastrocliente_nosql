using CadastroClienteApi.Models; // Seu namespace do projeto API
using CadastroClienteApi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// ====================================================================
// CONFIGURAÇÃO DOS SERVIÇOS (Injeção de Dependência)
// ====================================================================

// 1. Habilita o uso de Controllers COM SUPORTE A VIEWS
builder.Services.AddControllersWithViews();

// 2. Associa a seção "MongoDbSettings"
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// 3. Registra o serviço que interage com o MongoDB
builder.Services.AddSingleton<ClienteService>();

// Configuração padrão do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// ====================================================================
// CONFIGURAÇÃO DO PIPELINE DE REQUISIÇÃO (Middleware)
// ====================================================================

// Configura o Swagger UI para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Importante para CSS/JS (Estáticos)
app.UseRouting();
app.UseAuthorization();


// 4. Mapeamento de Controllers de API (se existirem rotas de API puras)
app.MapControllers(); 

// 5. Mapeamento da Rota Padrão MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // <--- ROTA PADRÃO MVC!

app.Run();