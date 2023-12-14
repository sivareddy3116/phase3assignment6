using System;
using System.Data.SqlClient;

namespace ProductInventorySystem
{
    class Program
    {
        static string connectionString = "server=DESKTOP-EBTO5CT;database=ProductInventoryDB;trusted_connection=true";

        static void Main(string[] args)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // View Product Inventory
                    Console.WriteLine("Product Inventory:");
                    ViewProductInventory(connection);

                    // Add New Product
                    Console.WriteLine("Add New Product:");
                    AddNewProduct(connection);

                    // Update Product Quantity
                    Console.WriteLine("Update Product Quantity:");
                    UpdateProductQuantity(connection);

                    // Remove Product
                    Console.WriteLine("Remove Product:");
                    RemoveProduct(connection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.ReadLine(); // Keep the console window open
            }
        }

        static void ViewProductInventory(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int productId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                        string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                        float price = (float)reader.GetDouble(reader.GetOrdinal("Price"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        DateTime mfDate = reader.GetDateTime(reader.GetOrdinal("MfDate"));
                        DateTime expDate = reader.GetDateTime(reader.GetOrdinal("ExpDate"));

                        Console.WriteLine($"ID: {productId}, Name: {productName}, Price: {price:C2}, Quantity: {quantity}, Manufacturing Date: {mfDate:dd-MM-yyyy}, Expiration Date: {expDate:dd-MM-yyyy}");
                    }
                }
            }

            Console.WriteLine();
        }

        static void AddNewProduct(SqlConnection connection)
        {
            Console.Write("Enter ProductId: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Price: ");
            float price = float.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Manufacturing Date (yyyy-MM-dd): ");
            DateTime mfDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Expiration Date (yyyy-MM-dd): ");
            DateTime expDate = DateTime.Parse(Console.ReadLine());

            using (SqlCommand command = new SqlCommand("INSERT INTO Products (ProductId, ProductName, Price, Quantity, MfDate, ExpDate) VALUES (@ProductId, @ProductName, @Price, @Quantity, @MfDate, @ExpDate)", connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@MfDate", mfDate);
                command.Parameters.AddWithValue("@ExpDate", expDate);

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0 ? "Product added successfully." : "Failed to add product.");
            }

            Console.WriteLine();
        }


        static void UpdateProductQuantity(SqlConnection connection)
        {
            Console.Write("Enter Product ID to update quantity: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Quantity: ");
            int newQuantity = int.Parse(Console.ReadLine());

            using (SqlCommand command = new SqlCommand("UPDATE Products SET Quantity = @NewQuantity WHERE ProductId = @ProductId", connection))
            {
                command.Parameters.AddWithValue("@NewQuantity", newQuantity);
                command.Parameters.AddWithValue("@ProductId", productId);

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0 ? "Quantity updated successfully." : "Failed to update quantity.");
            }

            Console.WriteLine();
        }

        static void RemoveProduct(SqlConnection connection)
        {
            Console.Write("Enter Product ID to remove: ");
            int productId = int.Parse(Console.ReadLine());

            using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductId = @ProductId", connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0 ? "Product removed successfully." : "Failed to remove product.");
            }

            Console.WriteLine();
        }
    }
}
