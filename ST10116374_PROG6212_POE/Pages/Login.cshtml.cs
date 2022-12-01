using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ST10116374_PROG6212_POE.Pages.IndexModel;
using System.Data.SqlClient;

namespace ST10116374_PROG6212_POE.Pages
{
    public class LoginModel : PageModel
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
                    string sql = "SELECT * FROM users" +
                                 "WHERE  name=@name AND password=@password";//this is used to validate the user is inputed correclt or if they need to register
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (ModelState.IsValid)
                        {
                            Response.Redirect("/Modules/Index");
                        }
                        else
                        {
                            Response.Redirect("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


        }
    }
}
