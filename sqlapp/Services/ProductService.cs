using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appserver231.database.windows.net";
        private static string db_user = "alejandropons";
        private static string db_password = "jefferson2673.";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            SqlConnection connection = GetConnection();

            string statement = "select * from Products";

            connection.Open();

            SqlCommand cmd = new SqlCommand(statement, connection);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(product);
                }
            }

            connection.Close();
            return products;
        }
    }
}
