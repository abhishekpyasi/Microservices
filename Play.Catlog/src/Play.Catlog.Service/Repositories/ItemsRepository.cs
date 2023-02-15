using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Play.Catlog.Service.Entities;

namespace Play.Catlog.Service.Repositories
{
    public class ItemsRepository
    {

        private const string collectionName = "Items"; // group of objects in DB its like table

        private readonly IMongoCollection<Item> DbCollection; // DB name

        private readonly FilterDefinitionBuilder<Item> filterbuilder = Builders<Item>.Filter; // to query info from Mongo DB

        public ItemsRepository()
        {
        }
    }
}