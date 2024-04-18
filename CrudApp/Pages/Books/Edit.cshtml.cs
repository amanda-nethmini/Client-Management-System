using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace CrudApp.Pages.Books
{
    public class EditModel : PageModel
    {
        public BookInfo bookInfo = new BookInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crudapp;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM books WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bookInfo.id = "" + reader.GetInt32(0);
                                bookInfo.title = reader.GetString(1);
                                bookInfo.author = reader.GetString(2);
                                bookInfo.genre = reader.GetString(3);
                                bookInfo.summary = reader.GetString(4); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            bookInfo.id = Request.Form["id"];
            bookInfo.title = Request.Form["title"];
            bookInfo.author = Request.Form["author"];
            bookInfo.genre = Request.Form["genre"];
            bookInfo.summary = Request.Form["summary"];

            if (bookInfo.id.Length == 0 || bookInfo.title.Length == 0 ||
            bookInfo.author.Length == 0 || bookInfo.genre.Length == 0
            || bookInfo.summary.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crudapp;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE books " +
                     "SET title=@title, author=@author, genre=@genre, summary=@summary " +
                    "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", bookInfo.title);
                        command.Parameters.AddWithValue("@author", bookInfo.author);
                        command.Parameters.AddWithValue("@genre", bookInfo.genre);
                        command.Parameters.AddWithValue("@summary", bookInfo.summary);
                        command.Parameters.AddWithValue("@id", bookInfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Books");
        }
    }
}