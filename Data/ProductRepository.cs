using System.Data;
using System.Data.SqlClient;
using ProjectShashtra.Models;
using Microsoft.Data.SqlClient;

namespace ProjectShashtra.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBCS");
        }
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = (int)reader["product_id"],
                        Name = reader["product_name"].ToString(),
                        Price = (decimal)reader["price"]


                    });
                }
            }
            return products;
        }

        public List<Product> GetProductsById(int id)
        {
            var products = new List<Product>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProductsById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = (int)reader["product_id"],
                        Name = reader["product_name"].ToString(),
                        Price = (decimal)reader["price"]


                    });
                }
            }
            return products;
        }

        public int InsertProducts(Product product)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_insertProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_id", product.Id);
                cmd.Parameters.AddWithValue("@product_name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock);
                con.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);

            }
        }

        public bool UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_updateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_id", product.Id);
                cmd.Parameters.AddWithValue("@product_name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
        }

        public bool DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_deleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_id", id);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }

        }

    }
}
