using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CadastroClienteApi.Models
{
    public class Cliente
    {
        // Define o Id como a chave primária do MongoDB (ObjectId)
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; } = string.Empty;
        [BsonElement("CPF")]
        public string CPF { get; set; } = string.Empty;
        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;
        // Outras propriedades...
        [BsonElement("Telefone")]
        public string Telefone { get; set; } = string.Empty;
        [BsonElement("Endereco")]
        public string Endereco { get; set; } = string.Empty;
    }
}