﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Core.Model
{
    /// <summary>
    /// Resim Tablosu
    /// </summary>
    public class ImageMongoModel : MongoBaseModel
    {

        /// <summary>
        /// Hangi DB ?
        /// </summary>
        [BsonElement]
        public string DatabaseName { get; set; }
        /// <summary>
        /// Hangi Tablo
        /// </summary>
        [BsonElement]
        public string TableName { get; set; }
        /// <summary>
        /// İçerik Id si
        /// </summary>
        [BsonElement]
        public int ParentId { get; set; }
        /// <summary>
        /// Küçük resim
        /// </summary>
        [BsonElement]
        public string SmallImage { get; set; }
        /// <summary>
        /// Büyük resim
        /// </summary>
        [BsonElement]
        public string LargeImage { get; set; }
        /// <summary>
        /// İçerik
        /// </summary>
        [BsonElement]
        public string Content { get; set; }
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
    }
}
