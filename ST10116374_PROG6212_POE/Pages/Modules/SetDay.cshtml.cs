using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ST10116374_PROG6212_POE.Pages.Modules.IndexModel;

namespace ST10116374_PROG6212_POE.Pages.Modules
{
    public class SetDayModel : PageModel
    {
        //calls the list from index
        public SetDateInfo listsetday = new SetDateInfo();
        public string errorMessage = "";          //calls error message when user enter wrong into the app
        public string succesMesage = "";          //calls succes message when user enter the right inputs into the app
        public int studyhrs;                      //int are needed for rieving data in the form of ints
        public void OnGet()
        {
        }

        public void Onpost(int studyhrs)
        {
            listsetday.sday = Request.Form["sday"];//this block is used to get the data from the web and make it know where to go
            listsetday.mdscode = Request.Form["mdscode"];
            listsetday.studyhrs = studyhrs;

            if (listsetday.sday.Length == 0 || listsetday.mdscode.Length == 0)
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
                    string sql = "INSERT INTO setaday" +
                                 "(day, mcode, study_hours) VALUES " +
                                 "(@sday, @mdscode, @studyhrs)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //these will insert the data both into the list and database
                        command.Parameters.AddWithValue("@sday", listsetday.sday);
                        command.Parameters.AddWithValue("@mdscode", listsetday.mdscode);
                        command.Parameters.AddWithValue("@studyhrs", listsetday.studyhrs);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            listsetday.sday = ""; listsetday.mdscode = "";
            succesMesage = "New Day Added Successfully";

           
        }
    }
}
