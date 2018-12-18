using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace bootcamp
{

    class Program
    {
        private const string account = "https://532lab.documents.azure.com:443/";
        private const string key = "AZHpvBPjWLZaWrwWvaC4n3dpAQ4rznDzrwMULY7FARdiY5UiLbRDCK7tAupUHFdo2iRSfd71lrP3Sd2LIXxBfw==";
        static void Main(string[] args)
        {
            TestDocDb.Wait();
            Console.WriteLine("Hello World!");
        }

        private static async Task TestDocDb()
        {
            DocumentClient _client = new DocumentClient(new Uri(account), key);
            string id = "SalesDB";
            var database = _client.CreateDatabaseQuery().Where(db => db.Id == id).AsEnumerable().
            FirstOrDefault();
            if (database == null)
            {
                database = await client.CreateDatabaseAsync(new Database { Id = id });
            }

        }
    }
}
