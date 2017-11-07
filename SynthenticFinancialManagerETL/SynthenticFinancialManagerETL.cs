using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthenticFinancialManager
{
    public class SynthenticFinancialManagerETL<T>
    {

        public IList<T> TXList { get; set; }
        public string TableName { get; set; }
        public int CommitSize { get; set; }
        public string ConnectionString { get; set; }

        public void Commit()
        {
            if (TXList.Count > 0)
            {
                DataTable dt;
                int numOfPages = (TXList.Count / CommitSize) + (TXList.Count % CommitSize == 0 ? 0 : 1);
                for (int page = 0; page < numOfPages; page++)
                {
                    dt = TXList.Skip(page * CommitSize).Take(CommitSize).ToDataTable();
                    BulkInsert(dt);
                }
            }
        }

        /// <summary>
        /// Make the bulk copy execution
        /// </summary>
        /// <param name="dt"></param>
        public void BulkInsert(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlBulkCopy bulkCopy =
                    new SqlBulkCopy
                    (
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );

                bulkCopy.DestinationTableName = TableName;
                connection.Open();

                // write in the "dataTable" 
                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
            // reset
            dt.Clear();
        }

    }
    /// <summary>
    /// Performs a list to datatable conversion
    /// </summary>
    public static class BulkUploadToSqlHelper
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
