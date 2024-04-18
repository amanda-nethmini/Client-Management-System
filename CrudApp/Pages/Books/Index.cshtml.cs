using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CrudApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<BookInfo> listBooks = new List<BookInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crudapp;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection  connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM books";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookInfo bookInfo = new BookInfo();
                                bookInfo.id = "" + reader.GetInt32(0);
                                bookInfo.title = reader.GetString(1);
                                bookInfo.author = reader.GetString(2);
                                bookInfo.genre = reader.GetString(3);
                                bookInfo.summary = reader.GetString(4);

                                listBooks.Add(bookInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
               Console.Write("Exception: " + ex.ToString());
            }
        }
    }

    public class BookInfo
    {
        public String id;
        public String title;
        public String author;
        public String genre;
        public String summary;
    }
}
