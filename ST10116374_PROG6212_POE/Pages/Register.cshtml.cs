using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ST10116374_PROG6212_POE.Pages.IndexModel;
using System.Data.SqlClient;

namespace ST10116374_PROG6212_POE.Pages
{
    public class RegisterModel : PageModel
    {
        //calls the list from index
        public UserInfo userinfo = new UserInfo();
        public string errorMessage = "";
        public string succesMesage = "";
        public void OnGet()
        {
        }
        public void Onpost()
        {
            userinfo.name = Request.Form["name"];
            userinfo.password = Request.Form["password"];

            if (userinfo.name.Length == 0 || userinfo.password.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the client into the database
            try
            {
                //This is the sql connection string you will need to change this to connect to the database
                string connectionString = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=modulesdb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO users" +
                                 "(name, password) VALUES " +
                                 "(@name, @password)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userinfo.name);
                        command.Parameters.AddWithValue("@password", userinfo.password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            userinfo.name = ""; userinfo.password = "";
            succesMesage = "New User Added Correctly";//message appears if user entered the rright input

            Response.Redirect("Index");
        }
    }
}
