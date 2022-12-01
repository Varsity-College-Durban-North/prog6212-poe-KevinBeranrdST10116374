using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ST10116374_PROG6212_POE.Pages.Modules.IndexModel;
namespace ST10116374_PROG6212_POE.Pages.Modules
{
    public class CreateModel : PageModel
    {
        //calls the list from index
        public ModuleInfo moduleinfo = new ModuleInfo();
        public string errorMessage = "";          //calls error message when user enter wrong into the app
        public string succesMesage = "";          //calls succes message when user enter the right inputs into the app
        public int credits;                       //int are needed for rieving data in the form of ints
        public int hours_per_week;
        public int weeks;
        public int current_hours;

        public void OnGet()
        {
        }


        public void Onpost(int credits, int hours_per_week, int weeks , int current_hours)
        {
            moduleinfo.mdcode = Request.Form["code"];//this block is used to get the data from the web and make it know where to go
            moduleinfo.mdname = Request.Form["name"];
            moduleinfo.credits = credits;
            moduleinfo.hourspweek = hours_per_week;
            moduleinfo.weeks = weeks;
            moduleinfo.startdate = Request.Form["start_date"];
            moduleinfo.CurrentHours = current_hours;
            
            //This is used to make sure the user enters the right dta into the text blocks and to not leave any open
            if (moduleinfo.mdcode.Length == 0 || moduleinfo.mdname.Length == 0 ||
                moduleinfo.startdate.Length == 0 || moduleinfo.credits <= 0 || moduleinfo.hourspweek <= 0 || moduleinfo.weeks <= 0)
            {
                errorMessage = "All the fields are required"; //this is the error message
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
                    string sql = "INSERT INTO modules" +
                                 "(code, name, credits, weeks, hours_per_week, start_date, current_hours) VALUES " +
                                 "(@code, @name, @credits, @weeks, @hours_per_week, @start_date, @current_hours)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //these will insert the data both into the list and database
                        command.Parameters.AddWithValue("@code", moduleinfo.mdcode);
                        command.Parameters.AddWithValue("@name", moduleinfo.mdname);
                        command.Parameters.AddWithValue("@credits", moduleinfo.credits);
                        command.Parameters.AddWithValue("@weeks", moduleinfo.weeks);
                        command.Parameters.AddWithValue("@hours_per_week", moduleinfo.hourspweek);
                        command.Parameters.AddWithValue("@start_date", moduleinfo.startdate);
                        command.Parameters.AddWithValue("@current_hours", moduleinfo.CurrentHours);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            moduleinfo.mdcode = ""; moduleinfo.mdname = ""; moduleinfo.startdate = "";
            succesMesage = "New Module Added Successfully";

            
        }
    }
}
