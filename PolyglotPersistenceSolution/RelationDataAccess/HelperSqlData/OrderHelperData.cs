using Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationDataAccess.HelperSqlData
{
    public static class OrderHelperData
    {
        public static SqlCommand CreateInsertChartCommand(this SqlCommand command,ChartModel chart)
        {
            DataTable products= new DataTable();
            products.Columns.Add("productId",typeof(long));
            foreach(var item in chart.Products)
            {
                products.Rows.Add(item.Id);
            }
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Products",products);
            command.Parameters.AddWithValue("@ConsumerId",chart.Consumer.Id);

            return command;
        }
    }
}
