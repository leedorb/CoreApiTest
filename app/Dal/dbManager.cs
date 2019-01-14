using app.Models;
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
        const string dbName = "sampledb";
        const string connectionString = "mongodb://mongouser:mongopass@mongodb-36-rhel7-shlomi1.leedor-test.svc.cluster.local:27017/" + dbName;
        readonly static IMongoCollection<Person> userCollection;

        static dbManager()
        {
            userCollection = setPersonCollection();
        }


        public static List<Person> GetPersonsList()
        {
            //get persons list
            var persons = userCollection.Find(p => true).ToList();

            return persons;
        }
        

        public static Person GetPerson(string id)
        {
            //get specific person by id
            var person = userCollection.Find(p => p.ID == id).FirstOrDefault();

            return person;
        }

        public static void InsertNewPerson(Person person)
        {
            if (GetPerson(person.ID) != null)
            {
                throw new Exception($"Person with id {person.ID} all ready exists");
            }

            userCollection.InsertOne(person);
        }

        public static void UpdatePerson(Person person)
        {
            if (GetPerson(person.ID) == null)
            {
                throw new Exception($"Person with id {person.ID} not exists");
            }

            DeletePerson(person.ID); //Delete old person
            InsertNewPerson(person); //Insert updated person
        }

        public static void DeletePerson(string id)
        {
            if (GetPerson(id) == null)
            {
                throw new Exception($"Person with id {id} not exists");
            }


            userCollection.DeleteOne(p => p.ID == id);
        }

        private static IMongoCollection<Person> setPersonCollection()
        {
            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase(dbName);

            //get mongodb collection
            var collection = database.GetCollection<Person>("Persons");

            return collection;
        }
    }
}
