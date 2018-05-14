using System;
using System.Collections.Generic;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Driver;
namespace vehicle_report_generator
{

    class Program
    {
        private static MongoClient client = new MongoClient();
        private static string host = "cloudsolution.documents.azure.com";
        private static string dbName = "driverdata";
        private static string userName = "cloudsolution";
        private static string password = "4YG3IpnaHL2kxa6xNRrYO9oftvkBZ3wekktf4CXNXhuLmtoS8aXbesXXhOrckqRy0unG7s9iBATwnzXX24tS2Q==";
        private static IMongoCollection<DriverData> driverCollection;
        private static string collectionName = "dailyreport";
        static void Main(string[] args)
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);
            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);
            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            driverCollection = database.GetCollection<DriverData>(collectionName);
            GetParticularDriverData();
            //InsertDriverData();
        }

        static void GetAllDriverData()
        {
            try
            {
                var temp = driverCollection.Find(new BsonDocument()).ToList();
            }
            catch(MongoConnectionException)
            {

            }
        }
        static void GetParticularDriverData()
        {
            var temp = driverCollection.Find("{vehicleId:{$eq:\"201\"}}").ToList();
        }
        static void InsertDriverData()
        {
            driverCollection.InsertOne(GetMockDriverData());
        }

        static DriverData GetMockDriverData()
        {
            var parkingSlots = new List<ParkingSlot>();
            parkingSlots.Add(new ParkingSlot() { X = 116.99465, Y = 38.69923 });
            parkingSlots.Add(new ParkingSlot() { X = 116.90198, Y = 38.60805 });

            var driverData = new DriverData()
            {
                CreatedDate = DateTime.Today,
                VehicleId = "201",
                ParkingSlots = parkingSlots,
                Report = new DailyInfo() { AverageMiles = 81.5, TotalMiles = 270 }
            };

            return driverData;
        }
    }
}
