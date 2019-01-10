using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Dal
{
    public class dbManager
    {
        //const string connectionString = "mongodb://localhost:27017";
        //const string connectionString = "mongodb://mongodb-36-rhel7-shlomi1.leedor-test.svc.cluster.local:27017";
        const string connectionString = "mongodb://mongouser:mongopass@mongodb-36-rhel7-shlomi1.leedor-test.svc.cluster.local:27017/" + dbName;
        const string dbName = "sampledb";

        public static List<Person> GetPersonsList()
        {
            
            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase(dbName);

            //get mongodb collection
            var collection = database.GetCollection<Person>("Persons");

            var persons = collection.Find(p => true).ToList();
            //await collection.InsertOneAsync(new Entity { Name = "Jack" });

            return persons;
        }

        public static Person GetPerson(string id)
        {

            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase(dbName);

            //get mongodb collection
            var collection = database.GetCollection<Person>("Persons");

            var person = collection.Find(p => p.ID == id).FirstOrDefault();
            //await collection.InsertOneAsync(new Entity { Name = "Jack" });

            return person;
        }

        public static async void InsertNewPerson(Person person)
        {

            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase(dbName);

            //get mongodb collection
            var collection = database.GetCollection<Person>("Persons");

            await collection.InsertOneAsync(person);
        }
    }

    [BsonIgnoreExtraElements]
    public class Person
    {
        //public ObjectId Id { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age  { get; set; }
        
    }
}
