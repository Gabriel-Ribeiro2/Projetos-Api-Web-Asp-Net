using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GenericoApi.Models
{
    public class Generico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("nome")]
        public string Nome { get; set; }
        [BsonElement("descricao")]
        public string Descricao { get; set; }
        [BsonElement("idade")]
        public int Idade { get; set; }
        [BsonElement("cancelado")]
        public bool Cancelado { get; set; }

    }
}
