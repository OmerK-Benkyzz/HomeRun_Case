using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Domain.Entities;

public class MongoDbBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public DateTime CreateTime { get; set; }
}