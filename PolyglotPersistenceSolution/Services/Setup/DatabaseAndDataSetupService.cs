using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ExternalData;
using Core.Models;
using Services.JsonWorker;

namespace Services.Setup
{
    public class DatabaseAndDataSetupService
    {
        private readonly string _databaseName;
        private readonly string _connectionString;

        private const string JSON_PATH = "Data";
        private const int SMALL_SET = 5_000;
        private const int MEDIUM_SET = 50_000;
        private const int LARGE_SET = 500_000;

        private readonly JsonWorkerClass _jsonWorker;

        public DatabaseAndDataSetupService(string databaseName)
        {
            _databaseName = databaseName;
            _connectionString = $"Data Source=.;Initial Catalog={_databaseName};Integrated Security=True;TrustServerCertificate=True;";
            _jsonWorker = new JsonWorkerClass(JSON_PATH);
        }

        public List<SellerModel> GetAllSellers()
        {
            var sellersRaw = _jsonWorker.ReadObjectsFromFile<SellerEx>("SellersRaw.json");
            var sellers= CompanyGeneratorService.ToSellers(sellersRaw);
            return sellers;
        }
    }
}
