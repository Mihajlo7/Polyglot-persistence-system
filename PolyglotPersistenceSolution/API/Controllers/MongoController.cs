using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{
    [ApiController]
    [Route("mongo")]
    public class MongoController : ControllerBase
    {
        string connectionString = "mongodb://127.0.0.1:27017";
        string databaseName = "test";
        string collectionName = "persons";

        [HttpGet]
        public IActionResult GetFirstMethod() 
        { 

            var client= new MongoClient(connectionString);
            var db= client.GetDatabase(databaseName);
            var collection = db.GetCollection<Person>(collectionName);

            var person = new Person { FirstName = "Nika", LastName = "Peric" };
            collection.InsertOne(person);

            //vraca sve rekorde
            var result = collection.Find(_=>true);
            return Ok(result.ToList());
        }
    }
}
