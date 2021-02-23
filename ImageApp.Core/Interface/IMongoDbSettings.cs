using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Core.Interface
{
   public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CollectionName { get; set; }
    }
}
