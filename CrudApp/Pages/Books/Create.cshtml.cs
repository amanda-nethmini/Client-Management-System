using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CrudApp.Pages.Books
{
    public class CreateModel : PageModel
    {
        public BookInfo bookInfo = new BookInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            bookInfo.title = Request.Form["title"];
            bookInfo.author = Request.Form["author"];
            bookInfo.genre = Request.Form["genre"];
            bookInfo.summary = Request.Form["summary"];

            if (bookInfo.title.Length == 0 || bookInfo.author.Length == 0 ||
                bookInfo.genre.Length == 0 || bookInfo.summary.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crudapp;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO books (title, author, genre, summary) VALUES (@title, @author, @genre, @summary)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", bookInfo.title);
                        command.Parameters.AddWithValue("@author", bookInfo.author);
                        command.Parameters.AddWithValue("@genre", bookInfo.genre);
                        command.Parameters.AddWithValue("@summary", bookInfo.summary);

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
