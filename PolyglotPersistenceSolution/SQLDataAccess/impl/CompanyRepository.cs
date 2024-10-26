using Core.ExternalData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess.impl
{
    public class CompanyRepository
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=small_database;Integrated Security=True;TrustServerCertificate=True;";

        public async Task<int> InsertManySellers(List<SellerEx> sellers)
        {
            int count = 0;
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("insertSeller", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            foreach (SellerEx seller in sellers)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@DunsNumber", seller.DunsNumber);
                command.Parameters.AddWithValue("@Name",seller.Name);
                command.Parameters.AddWithValue("@Telephone", seller.Telephone);
                command.Parameters.AddWithValue("@Country", seller.Country);
                command.Parameters.AddWithValue("@Address", seller.Address);
                command.Parameters.AddWithValue("@City", seller.City);
                command.Parameters.AddWithValue("@HasShop", seller.HasStore);

                await command.ExecuteNonQueryAsync();
                count++;
            }

            return count;
        }

        public async Task InsertBulkCouriers(List<CourierEx> couriers)
        {
            
            using var connection = new SqlConnection(connectionString);
            using var copy = new SqlBulkCopy(connection);
            var (companyTable,courierTable)= toTableCouriers(couriers);
            copy.DestinationTableName = "Companies";

            copy.ColumnMappings.Add(nameof(CourierEx.Id), "Id");
            copy.ColumnMappings.Add(nameof(CourierEx.DunsNumber), "dunsNumber");
            copy.ColumnMappings.Add(nameof(CourierEx.Name), "name");
            copy.ColumnMappings.Add(nameof(CourierEx.Telephone), "telephone");
            copy.ColumnMappings.Add(nameof(CourierEx.Country), "country");
            connection.Open();
            await copy.WriteToServerAsync(companyTable);

            copy.DestinationTableName = "Couriers";
            copy.ColumnMappings.Clear();

            copy.ColumnMappings.Add(nameof(CourierEx.Id), "id");
            copy.ColumnMappings.Add(nameof(CourierEx.DeliveryPrice), "deliveryPrice");

            await copy.WriteToServerAsync(courierTable);

        }

        private (DataTable,DataTable) toTableCouriers(List<CourierEx> couriers)
        {
            var companyTable= new DataTable();
            companyTable.Columns.Add("Id", typeof(Guid));
            companyTable.Columns.Add("dunsNumber", typeof(string));
            companyTable.Columns.Add("name", typeof(string));
            companyTable.Columns.Add("telephone", typeof(string));
            companyTable.Columns.Add("country", typeof(string));

            var courierTable = new DataTable();
            courierTable.Columns.Add("deliveryPrice",typeof(decimal));
            courierTable.Columns.Add("id", typeof(Guid));

            foreach (var courier in couriers)
            {
                companyTable.Rows.Add(courier.Id,courier.DunsNumber,courier.Name,courier.Telephone,courier.Country);
                courierTable.Rows.Add(courier.DeliveryPrice,courier.Id);
            }

            return (companyTable, courierTable);

        }
    }
}
