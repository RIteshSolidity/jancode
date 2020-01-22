using System;
using Microsoft.Azure.Cosmos;

namespace CosmosDemo
{
    class Program
    {
        private static string connectionString = "AccountEndpoint=https://coresqlmclassjan.documents.azure.com:443/;AccountKey=vuoy4vp9FJ6t20JJgiIbPopWbtURY37EHyEzMgxbMvJ4sdzDbJhj43wwHs1ZkN56grw5DC5GHECpE7WLv0e9hg==;";
        private static CosmosClient client;
        static void Main(string[] args)
        {
            CosmosClientOptions options = new CosmosClientOptions {
                ConsistencyLevel = ConsistencyLevel.Eventual
                
            };
            client = new CosmosClient(connectionString, options);

            DatabaseResponse dr = client.CreateDatabaseIfNotExistsAsync("TestDatabase", 500).GetAwaiter().GetResult();
            UniqueKey key1 = new UniqueKey();
            key1.Paths.Add("/lastname");

            UniqueKeyPolicy policy = new UniqueKeyPolicy();
            policy.UniqueKeys.Add(key1);

            ContainerProperties cp = new ContainerProperties
            {
                Id = "testcontainer",
                PartitionKeyPath = "/email",
                UniqueKeyPolicy = policy,
                
            };
            cp.IndexingPolicy.ExcludedPaths.Add(new ExcludedPath() { Path = "/*" });
            cp.IndexingPolicy.IncludedPaths.Add(new IncludedPath() { Path = "/firstname/*"});
            ContainerResponse cr =  dr.Database.CreateContainerIfNotExistsAsync(cp, 600).GetAwaiter().GetResult();
            employee e = new employee {
                 age = 10,
                 email = "aaa@aaa.com",
                 firstname = "ritesh1",
                 id = "003",
                 lastname = "modi3"
            };

            ItemResponse<employee> ir =  cr.Container.CreateItemAsync<employee>(e, new PartitionKey("aaa@aaa.com")).GetAwaiter().GetResult();
            Console.WriteLine(ir.Resource.email.ToString());
            Console.WriteLine(ir.RequestCharge.ToString());
            QueryRequestOptions ro = new QueryRequestOptions {
                MaxItemCount = 1
            };

            FeedIterator<employee> fi =  cr.Container.GetItemQueryIterator<employee>("select * from c", null, ro);
            while (fi.HasMoreResults) {
                FeedResponse<employee> fr = fi.ReadNextAsync().GetAwaiter().GetResult();
                foreach (var i in fr) {
                    Console.WriteLine(i.email + " " + i.firstname);
                }
            }

        }
    }

    public class employee {
        public string id { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
    }
}
