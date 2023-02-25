using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Catlog.Service.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }

        public string ConnectionString => $"mongodb://{Host}:{Port}"; // expression body definition . its property 
        // defined directly by valur on right side.
    }
}