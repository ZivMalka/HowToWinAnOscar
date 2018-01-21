using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.DB
{
    public class DBManager
    {

        static MongoClient client;
        static IMongoDatabase DB;

        static DBManager()
        {
            client = new MongoClient("mongodb://localhost:27017");
            DB = client.GetDatabase("test");
        }

        public static IMongoCollection<Attributes> GetOscarCollection()
        {
            var collection = DB.GetCollection<Attributes>("Oscar");
            return collection;
        }

        public static IMongoCollection<Users> GetUsersCollection()
        {
            var collection = DB.GetCollection<Users>("User");
            return collection;
        }

   

  
    }
}