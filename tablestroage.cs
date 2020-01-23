using System;
using Microsoft.Azure.Cosmos.Table;


namespace ConsoleApp19
{
    class Program
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=mclassjanstorage;AccountKey=RhWDpGr7nL5jm7ZJKP0yQp2eWoKFpM82QCLFTrhiBVDA9Cr0PbqkpFxECXMEKEDxUgZ+23H5nR0GHv7ZD6zxAg==;EndpointSuffix=core.windows.net";
        private static string tableName = "employeeTable";
        private static CloudStorageAccount csa;
        private static CloudTableClient ctc;
        private static CloudTable ct;
        static void Main(string[] args)
        {
            csa = CloudStorageAccount.Parse(connectionString);
            ctc = csa.CreateCloudTableClient();
            ct = ctc.GetTableReference(tableName);
            ct.CreateIfNotExistsAsync().GetAwaiter().GetResult();

            TableQuery<employee> tq = new TableQuery<employee>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal , "hyderabad")
                );

            var employees = ct.ExecuteQuery<employee>(tq);
            foreach(var e in employees)
            {
                Console.WriteLine(e.PartitionKey);

            }
          //  employee e = new employee("hyderabad", "aa@aaaaa.com") {
          //      firstname = "ritesh",
          //      lastname = "modi"               
          //  };

          //  TableOperation op1 = TableOperation.Insert(e);
          //  ct.ExecuteAsync(op1).GetAwaiter().GetResult();

          //  employee e1 = new employee("hyderabad", "aa@aaaa.com")
          //  {
          //      firstname = "ritesh1",
          //      lastname = "modi1"
          //  };

          //  TableOperation op2 = TableOperation.Insert(e1);
          ////  ct.ExecuteAsync(op2).GetAwaiter().GetResult();

          //  employee e2 = new employee("hyderabad", "aa@aaa.com")
          //  {
          //      firstname = "ritesh2",
          //      lastname = "modi2"
          //  };

          //  TableOperation op3 = TableOperation.Insert(e2);
          //// ct.ExecuteAsync(op3).GetAwaiter().GetResult();

          //  TableBatchOperation bop = new TableBatchOperation();
          //  bop.Add(op2);
          //  bop.Add(op3);

          //  ct.ExecuteBatchAsync(bop).GetAwaiter().GetResult();
        }
    }

    public class employee: TableEntity {

        public employee()
        {

        }
        public employee(string _location, string _email)
        {
            PartitionKey = _location;
            RowKey = _email;
        }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string email { get; set; }
        public string location { get; set; }
    }
}
