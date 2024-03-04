using Microsoft.Azure.Cosmos;
using System.Net;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace ConsoleApp1
{
    public class Entity
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        public string Name { get; set; }
        public ICollection<string>? FeautureIds { get; set; }
        public string UniqueIdentifier { get; set; } //one of the columns - like primary key
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
    internal class Program
    {
        static async void CreateItem()
        {
            using (CosmosClient cosmosClient = new CosmosClient())//connection string
            {
                Entity entity = new Entity() { id = Guid.NewGuid().ToString(), Name = "sample",FeautureIds=new List<string>() { "s1", "s2", "s3" }
                ,Created=DateTime.Now,Updated=DateTime.Now};
                Container container = cosmosClient.GetContainer("feature-store", "Entity");
                // Read item from container
                ItemResponse<Entity> createResponse = await container.CreateItemAsync<Entity>(entity);
                HttpStatusCode statusCode = createResponse.StatusCode;
                Console.WriteLine(statusCode.ToString());
            }
        }
        static void Main(string[] args)
        {
         
            CreateItem();
            while (true)
            {

            }
        }
    }
}
