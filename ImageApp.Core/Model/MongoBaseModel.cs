using ImageApp.Core.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Core.Model
{
    /// <summary>
    /// MongoDB Kayıtları için Base Model
    /// </summary>
    public class MongoBaseModel : IMongoBaseModel
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
