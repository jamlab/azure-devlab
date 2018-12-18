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
            //CreateDB().Wait();
            Console.WriteLine("DB & Data created...");
            Console.ReadKey();
        }
        private static async Task TestDocDb()
        {
     

            await CreateDB();

        }

        private static async Task CreateDB()
        {
            //Create DB
            DocumentClient _client = new DocumentClient(new Uri(account), key);
            DocumentClient client = new DocumentClient(new Uri(account), key);
            string id = "SalesDB";
            var database = _client.CreateDatabaseQuery().Where(db => db.Id == id).AsEnumerable().
            FirstOrDefault();
            if (database == null) {
                database = await client.CreateDatabaseAsync(new Database { Id = id });
            }

            //Create Collection
            string collectionName = "Customers";
            var collection = client.CreateDocumentCollectionQuery(database.CollectionsLink).
            Where(c => c.Id == collectionName).AsEnumerable().FirstOrDefault();
            if (collection == null) {
                collection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                new DocumentCollection { Id = collectionName });
            }

            var contoso = new Customer {
                CustomerName = "Contoso Corp",
                PhoneNumbers = new PhoneNumber[]{
                    new PhoneNumber
                    {   CountryCode = "1",
                        AreaCode = "619",
                        MainNumber = "555-1212" },
                    new PhoneNumber
                    {
                        CountryCode = "1",
                        AreaCode = "760",
                        MainNumber = "555-2442" },
                    }
            };
            var wwi = new Customer {
                CustomerName = "World Wide Importers",
                PhoneNumbers = new PhoneNumber[]{
                    new PhoneNumber{
                        CountryCode = "1",
                        AreaCode = "858",
                        MainNumber = "555-7756" },
                    new PhoneNumber{
                        CountryCode = "1",
                        AreaCode = "858",
                        MainNumber = "555-9142" },
                }
            };
            await client.CreateDocumentAsync(collection.DocumentsLink, contoso);
            await _client.CreateDocumentAsync(collection.DocumentsLink, wwi);
        }
    }
    public class PhoneNumber
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string MainNumber { get; set; }
    }
    public class Customer
    {
        public string CustomerName { get; set; }
        public PhoneNumber[] PhoneNumbers { get; set; }
    }
}
