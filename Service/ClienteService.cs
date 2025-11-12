using CadastroClienteApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace CadastroClienteApi.Services
{
    public class ClienteService
    {
        // Variável de coleção (Coleção = Tabela no SQL)
        private readonly IMongoCollection<Cliente> _clientesCollection;
        // Construtor que recebe as configurações
        public ClienteService(
            IOptions<MongoDbSettings> mongoDbSettings)
        {
            // Cria o cliente e acessa o banco de dados
            var mongoClient = new MongoClient(
                mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbSettings.Value.DatabaseName);
            // Acessa a coleção 'Clientes'
            _clientesCollection = mongoDatabase.GetCollection<Cliente>(
                mongoDbSettings.Value.CollectionName);
        }
        // --- OPERAÇÕES CRUD BÁSICAS ---
        // Ler todos os clientes
        public async Task<List<Cliente>> GetAsync() =>
            await _clientesCollection.Find(_ => true).ToListAsync();
        // Ler cliente por ID
        public async Task<Cliente?> GetAsync(string id) =>
            await _clientesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        // Criar um novo cliente (C - Create)
        public async Task CreateAsync(Cliente novoCliente) =>
            await _clientesCollection.InsertOneAsync(novoCliente);
        // Atualizar um cliente (U - Update)
        public async Task UpdateAsync(string id, Cliente clienteAtualizado) =>
            await _clientesCollection.ReplaceOneAsync(x => x.Id == id, clienteAtualizado);
        // Deletar um cliente (D - Delete)
        public async Task RemoveAsync(string id) =>
            await _clientesCollection.DeleteOneAsync(x => x.Id == id);
    }
}