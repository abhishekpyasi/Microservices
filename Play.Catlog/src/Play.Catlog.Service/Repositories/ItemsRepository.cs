using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Play.Catlog.Service.Entities;

namespace Play.Catlog.Service.Repositories
{
    public class ItemsRepository
    {

        private const string collectionName = "Items"; // group of objects in DB its like table

        private readonly MongoCollectionBase<Item> dbCollection; // represent a Type collection

        private readonly FilterDefinitionBuilder<Item> filterbuilder = Builders<Item>.Filter; // to query info from Mongo DB you need to create filter 

        public ItemsRepository()
        {
            //connect to mongodb

            var mongoClient = new MongoClient("mongodb://localhost:27017"); // to connect to DB with connection string
            var database = mongoClient.GetDatabase("Catalog"); // Database object
            // dbcollection object for doing operation
            dbCollection = (MongoCollectionBase<Item>)database.GetCollection<Item>(collectionName);

        }
        // client is expecting readonly collection of items so return type is Task of IReadonlyCollection
        // FilterDefinitions are a fast and intuitive way to create filters. 
        //They require less processing then using expressions and offer a lot of methods for you to use.

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()

        {

            // to get all items
            // find ,replaceoneasync , removeoneasync method need filter parameter to 
            return await dbCollection.Find(filterbuilder.Empty).ToListAsync();

        }

        public async Task<Item> GetItem(Guid id)
        {
            // create equality filter for finding record associated with ID
            FilterDefinition<Item> filter = filterbuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item entity) // not returning anything to  client no type parameter for Task
        {

            if (entity == null)
            {

                throw new ArgumentNullException(nameof(entity));

            }
            await dbCollection.InsertOneAsync(entity);

        }

        public async Task UpdateAsync(Item entity)
        {
            if (entity == null)
            {

                throw new ArgumentNullException(nameof(entity));

            }

            FilterDefinition<Item> filter = filterbuilder.Eq(existingentity => existingentity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);


        }

        public async Task RemoveAsync(Guid id)
        {

            FilterDefinition<Item> filter = filterbuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }

    }
}