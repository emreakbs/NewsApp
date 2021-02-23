using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Core.Interface
{
    /// <summary>
    /// Mongo DB için Base olarak kullanılacak
    /// </summary>
    public interface IMongoBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
