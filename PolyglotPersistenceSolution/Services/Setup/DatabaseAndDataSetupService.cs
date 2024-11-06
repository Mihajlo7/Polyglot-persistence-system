using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.ExternalData;
using Core.Models;
using Microsoft.Data.SqlClient;
using Services.JsonWorker;
using Services.Scripts;

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

        public void SetupRelationDatabase()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            var createTablesCommand = new SqlCommand(RemoveGoStatements(SetupSQLQueiries.Tables),connection,transaction);
            var createTriggersCommand = new SqlCommand(RemoveGoStatements(SetupSQLQueiries.Triggers), connection, transaction);
            var createConsumersProcedures = new SqlCommand(RemoveGoStatements(SetupSQLQueiries.ConsumersSP),connection,transaction);
            var createCompaniesProcedures = new SqlCommand(RemoveGoStatements(SetupSQLQueiries.CompanySP), connection, transaction);
            var createProductsProcedures = new SqlCommand(RemoveGoStatements(SetupSQLQueiries.ProductsSP), connection,transaction);
            var createOrdersProcedures = new SqlCommand(RemoveGoStatements(SetupSQLQueiries.OrderSP), connection, transaction);

            try
            {
                createTablesCommand.ExecuteNonQuery();
                createTriggersCommand.ExecuteNonQuery();
                createConsumersProcedures.ExecuteNonQuery();
                createCompaniesProcedures.ExecuteNonQuery();
                createProductsProcedures.ExecuteNonQuery();
                createOrdersProcedures.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally 
            {
                connection.Close();
            }
        }
        private string RemoveGoStatements(string sqlScript)
        {
            string pattern = @"^\s*GO\s*$";
            return Regex.Replace(sqlScript, pattern, string.Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase).Trim();
        }
    }
}
