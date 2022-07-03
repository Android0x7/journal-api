using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JournalAPI.Models;

public class JournalEntry {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("entry")]
    [BsonIgnoreIfNull]
    public string Entry { get; set; } = null!;

    [BsonElement("timestamp")]
    [BsonIgnoreIfNull]
    public string TimeStamp { get; set; } = null!;

    [BsonElement("tags")]
    [BsonIgnoreIfNull]
    public List<string> Tags { get; set; } = null!;

}