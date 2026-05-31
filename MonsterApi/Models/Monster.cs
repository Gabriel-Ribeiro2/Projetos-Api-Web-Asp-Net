using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MonsterApi.Models
{
    public class Monster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("nome")]
        public string Name { get; set; }
        [BsonElement("descricao")]
        public string Descricao { get; set; }
        [BsonElement("zero")]
        public bool ZeroAcucar { get; set; }
        [BsonElement("cancelado")]
        public bool Cancelado { get; set; }
        [BsonElement("Nota do Pietro")]
        public int NotaPietro { get; set; }

        
    }
}
